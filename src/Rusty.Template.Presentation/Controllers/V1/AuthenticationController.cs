#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.ProjectModel;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Application.Services;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Contracts.Requests.Authentication;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Options;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public sealed class AuthenticationController : BaseApiController
{
	private readonly IUserRepo _userRepo;
	private readonly IAuthenticationService _authenticationService;

	public AuthenticationController(IUserRepo userRepo, IAuthenticationService authenticationService)
	{
		_userRepo = userRepo;
		_authenticationService = authenticationService;
	}

	[SwaggerOperation(
		Summary = "Login",
		Description = "Logins to retrieve jwt token"
	)]
	[SwaggerResponse(
		StatusCodes.Status200OK,
		"Token retrieved successfully",
		typeof(TokenResponse)
	)]
	[SwaggerResponse(
		StatusCodes.Status401Unauthorized,
		"Login or password is incorrect",
		typeof(string)
	)]
	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
	{
		var user = await _userRepo.GetByUsernameAsync(request.Username, cancellationToken, includes => includes.Include(item => item.Roles).AsTracking().Include(item => item.RefreshToken));
		if (user == null || user.Password != request.Password)
			return Unauthorized("Login or password is incorrect");
		var claims = _authenticationService.GetClaimsForJwt(user, user.Roles.Select(item => item.Name));
		var tokenString = _authenticationService.GenerateJwtToken(claims);
		user.RefreshToken = _authenticationService.GenerateRefreshToken();
		await _userRepo.SaveChangesAsync();

		return Ok(new TokenResponse(tokenString, user.RefreshToken.Token));
	}
	[SwaggerResponse(
		StatusCodes.Status200OK,
		"Token retrieved successfully",
		typeof(TokenResponse)
	)]
	[SwaggerResponse(
		StatusCodes.Status202Accepted,
		"Access token has not expired yet",
		typeof(string)
	)]
	[SwaggerResponse(
		StatusCodes.Status401Unauthorized,
		"Refresh token is expired",
		typeof(string)
	)]
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest,
												  CancellationToken cancellationToken)
	{
		var principal = _authenticationService.GetPrincipalFromToken(refreshTokenRequest.JwtToken);
		if (principal == null)
			return BadRequest("Invalid access token or refresh token");
		var username = principal.Identity!.Name!;
		var user = await _userRepo.GetByUsernameAsync(username, cancellationToken, includes => includes.Include(item => item.RefreshToken)!.AsTracking());
		if (user == null || user.RefreshToken is null || user.RefreshToken.Token != refreshTokenRequest.RefreshToken)
			return BadRequest("Invalid access token or refresh token");
		var accessExpirationDateUnix = long.Parse(principal.FindFirst("exp")!.Value);
		var accessExpirationDate = DateTimeOffset.FromUnixTimeSeconds(accessExpirationDateUnix).UtcDateTime;
		if (accessExpirationDate >= DateTime.UtcNow)
			return Accepted("Access token has not expired yet");
		if (user.RefreshToken.ExpirationDate <= DateTime.UtcNow)
			return Unauthorized("Refresh token is expired. You need to login again");
		var newAccessToken = _authenticationService.GenerateJwtToken(principal.Claims);

		user.RefreshToken = _authenticationService.GenerateRefreshToken();
		await _userRepo.SaveChangesAsync();

		return Ok(new TokenResponse(newAccessToken, user.RefreshToken.Token));
	}
}