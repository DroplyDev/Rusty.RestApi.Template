using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rusty.Template.Application.Services;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Options;

namespace Rusty.Template.Infrastructure.Services;
public class AuthenticationService : IAuthenticationService
{
	private readonly AuthOptions _authOptions;
	private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512;
	public AuthenticationService(IOptions<AuthOptions> authOptions)
	{
		_authOptions = authOptions.Value;
	}
	public List<Claim> GetClaimsForJwt(User user, IEnumerable<string> roles)
	{
		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.UniqueName, user.UserName),
			new(JwtRegisteredClaimNames.Email, user.Email),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};
		claims.AddRange(roles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

		return claims;
	}

	public string GenerateJwtToken(IEnumerable<Claim> claims)
	{
		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret));

		var token = new JwtSecurityToken(
			_authOptions.Issuer,
			_authOptions.Audience,
			expires: DateTime.Now.Add(_authOptions.TokenLifetime.ToTimeSpan()),
			claims: claims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithm)
		);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public RefreshToken GenerateRefreshToken()
	{
		var length = 100;
		const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
		var sb = new StringBuilder();
		var random = new Random();
		while (0 < length--)
		{
			sb.Append(validChars[random.Next(validChars.Length)]);
		}
		return new RefreshToken
		{
			Token = sb.ToString(),
			CreationDate = DateTime.Now,
			ExpirationDate = DateTime.Now.Add(_authOptions.RefreshTokenLifetime.ToTimeSpan()),
			IsUsed = false,
			IsInvalidated = false
		};
	}
	public ClaimsPrincipal? GetPrincipalFromToken(string token)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = _authOptions.ValidateAudience,
			ValidateIssuer = _authOptions.ValidateIssuer,
			ValidateIssuerSigningKey = _authOptions.ValidateSecret,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret)),
			ValidateLifetime = _authOptions.ValidateAccessTokenLifetime
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		try
		{
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
			return IsValidJwtSecurityAlgorithm(securityToken) ? null : principal;
		}
		catch (Exception)
		{
			return null;
		}

	}

	private bool IsValidJwtSecurityAlgorithm(SecurityToken securityToken)
	{
		return securityToken is not JwtSecurityToken jwtSecurityToken ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithm,
					StringComparison.InvariantCultureIgnoreCase);
	}
}
