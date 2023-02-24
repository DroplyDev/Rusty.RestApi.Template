#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

[SwaggerSchema("Login response")]
public sealed record TokenResponse([SwaggerSchema("JWT token for authentication")]
								   string JwtToken,
								   [SwaggerSchema("Refresh token")]
								   string RefreshToken);