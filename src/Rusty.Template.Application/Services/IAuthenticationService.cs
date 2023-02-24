using Rusty.Template.Domain;
using System.Security.Claims;

namespace Rusty.Template.Application.Services;
public interface IAuthenticationService
{
	List<Claim> GetClaimsForJwt(User user, IEnumerable<string> roles);
	string GenerateJwtToken(IEnumerable<Claim> claims);
	RefreshToken GenerateRefreshToken();
	ClaimsPrincipal? GetPrincipalFromToken(string token);
}
