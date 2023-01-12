namespace Rusty.Template.Presentation.Options;

public class AuthOptions
{
	public string Issuer { get; set; } = null!;


	public bool ValidateIssuer { get; set; }


	public string Audience { get; set; } = null!;


	public bool ValidateAudience { get; set; }


	public string Key { get; set; } = null!;


	public bool ValidateKey { get; set; }


	public int AccessTokenLifetime { get; set; }


	public bool ValidateAccessTokenLifetime { get; set; }
}