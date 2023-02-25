namespace Rusty.Template.Contracts.Requests.Authentication;

/// <summary>
/// RefreshTokenRequest
/// </summary>
public sealed class RefreshTokenRequest
{
	/// <summary>The auth token.</summary>
	/// <example>JwtTokenString</example>
	public string JwtToken { get; set; } = null!;
	/// <summary>The refresh token.</summary>
	/// <value>RefreshTokenString</value>
	public string RefreshToken { get; set; } = null!;
}