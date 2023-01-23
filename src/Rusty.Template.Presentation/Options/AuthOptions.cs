namespace Rusty.Template.Presentation.Options;

public sealed record AuthOptions
{
	public string Issuer { get; init; } = null!;
	public bool ValidateIssuer { get; init; }
	public string Audience { get; init; } = null!;
	public bool ValidateAudience { get; init; }
	public string Key { get; init; } = null!;
	public bool ValidateKey { get; init; }
	public int TokenValidityInMinutes { get; init; }
	public bool ValidateAccessTokenLifetime { get; init; }
	public int RefreshTokenValidityInDays { get; init; }
}