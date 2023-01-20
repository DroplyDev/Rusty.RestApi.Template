namespace Rusty.Template.Contracts.Requests.Authentication;

public sealed class LoginRequest
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
}