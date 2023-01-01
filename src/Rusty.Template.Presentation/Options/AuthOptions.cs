namespace Rusty.Template.Presentation.Options;

/// <summary>
///     JWT Bearer authentication options
/// </summary>
public class AuthOptions
{
    /// <summary>
    ///     Organization name or some other issuer identity
    /// </summary>
    public string Issuer { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the validate issuer
    /// </summary>
    public bool ValidateIssuer { get; set; }


    /// <summary>
    ///     Target audience, for example web site, mobile application, desktop application in any short form preferred
    /// </summary>
    public string Audience { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the validate audience
    /// </summary>
    public bool ValidateAudience { get; set; }


    /// <summary>
    ///     Private symmetrical encryption key
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the value of the validate key
    /// </summary>
    public bool ValidateKey { get; set; }

    /// <summary>
    ///     Access token lifetime in minutes
    /// </summary>
    public int AccessTokenLifetime { get; set; }

    /// <summary>
    ///     Gets or sets the value of the validate access token lifetime
    /// </summary>
    public bool ValidateAccessTokenLifetime { get; set; }
}