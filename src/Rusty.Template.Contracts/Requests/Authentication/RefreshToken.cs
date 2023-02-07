namespace Rusty.Template.Contracts.Requests.Authentication;
public sealed class RefreshTokenRequest
{
    public string JwtToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;

}
