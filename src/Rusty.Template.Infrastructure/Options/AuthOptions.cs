namespace Rusty.Template.Infrastructure.Options;

public sealed record AuthOptions
{
	public string Secret { get; init; } = null!;
	public bool ValidateSecret { get; init; }
	public TimeOnly TokenLifetime { get; init; }
	public bool ValidateAccessTokenLifetime { get; init; }
	public TimeOnly RefreshTokenLifetime { get; init; }
	public string Issuer { get; init; } = null!;
	public bool ValidateIssuer { get; init; }
	public string Audience { get; init; } = null!;
	public bool ValidateAudience { get; init; }
}