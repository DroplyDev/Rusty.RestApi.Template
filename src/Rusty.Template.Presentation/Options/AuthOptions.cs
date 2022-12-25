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
    ///     Target audience, for example web site, mobile application, desktop application in any short form preferred
    /// </summary>
    public string Audience { get; set; } = null!;

    /// <summary>
    ///     Private symmetrical encryption key
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    ///     Access token lifetime in minutes
    /// </summary>
    public int AccessTokenLifetime { get; set; }
}