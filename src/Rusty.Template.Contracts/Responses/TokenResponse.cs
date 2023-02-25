#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

/// <summary>
/// Login response
/// </summary>
public sealed record TokenResponse
{
	/// <summary>JWT token for authentication.</summary>
	public string JwtToken { get; init; } = null!;
	/// <summary>Refresh token.</summary>
	public string RefreshToken { get; init; } = null!;

	/// <summary>Initializes a new instance of the <see cref="TokenResponse"/> class.</summary>
	/// <param name="jwtToken">The JWT token.</param>
	/// <param name="refreshToken">The refresh token.</param>
	public TokenResponse(string jwtToken, string refreshToken)
	{
		JwtToken = jwtToken;
		RefreshToken = refreshToken;
	}
}