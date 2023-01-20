namespace Rusty.Template.Contracts.Responses;

public sealed record LoginResponse(string JwtToken, DateTime JwtTokenExpiration, string RefreshToken);