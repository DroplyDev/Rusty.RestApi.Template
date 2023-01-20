#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Requests.Authentication;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Presentation.Options;

#endregion

namespace Rusty.Template.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class AuthenticationController : BaseApiController
{
	private readonly AuthOptions _authOptions;
	private readonly IUserRepo _userRepo;

	public AuthenticationController(IOptions<AuthOptions> authOptions, IUserRepo userRepo)
	{
		_userRepo = userRepo;
		_authOptions = authOptions.Value;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
	{
		var user = await _userRepo.FirstOrDefaultAsync(item => item.UserName == request.Username, cancellationToken,
			items => items.AsNoTracking().Include(item => item.Group)!);
		if (user != null && user.Password == request.Password)
		{
			var authClaims = new List<Claim>
			{
				new(ClaimTypes.Name, user.UserName),
				new(ClaimTypes.Email, user.Email),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};
			var userRoles = user.Roles.Select(item => item.Name);

			foreach (var userRole in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, userRole));

			var token = CreateToken(authClaims);
			var refreshToken = GenerateRefreshToken();

			// user.RefreshToken = refreshToken;
			// user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_authOptions.RefreshTokenValidityInDays);

			await _userRepo.UpdateAsync(user);
			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return Ok(new LoginResponse(tokenString, token.ValidTo, refreshToken));
		}

		return Unauthorized();
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest,
												  CancellationToken cancellationToken)
	{
		var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.JwtToken);
		if (principal == null) return BadRequest("Invalid access token or refresh token");
		var username = principal.Identity!.Name!;
		var user = await _userRepo.GetByUsernameAsync(username, cancellationToken);

		// if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
		// {
		// 	return BadRequest("Invalid access token or refresh token");
		// }

		var newAccessToken = CreateToken(principal.Claims.ToList());
		var newRefreshToken = GenerateRefreshToken();

		// user.RefreshToken = newRefreshToken;
		// await _userManager.UpdateAsync(user);

		return new ObjectResult(new
		{
			accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
			refreshToken = newRefreshToken
		});
	}

	private JwtSecurityToken CreateToken(List<Claim> authClaims)
	{
		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));

		var token = new JwtSecurityToken(
			_authOptions.Issuer,
			_authOptions.Audience,
			expires: DateTime.Now.AddMinutes(_authOptions.TokenValidityInMinutes),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		);

		return token;
	}

	private static string GenerateRefreshToken()
	{
		var randomNumber = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}

	private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = _authOptions.ValidateAudience,
			ValidateIssuer = _authOptions.ValidateIssuer,
			ValidateIssuerSigningKey = _authOptions.ValidateKey,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key)),
			ValidateLifetime = _authOptions.ValidateAccessTokenLifetime
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
		if (securityToken is not JwtSecurityToken jwtSecurityToken ||
		    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
			    StringComparison.InvariantCultureIgnoreCase))
			throw new SecurityTokenException("Invalid token");

		return principal;
	}
}