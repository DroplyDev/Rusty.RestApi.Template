#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

[SwaggerSchema("Login response")]
public sealed record LoginResponse([SwaggerSchema("JWT token for authentication")]
								   string JwtToken,
								   [SwaggerSchema("JWT token expiration date")]
								   DateTime JwtTokenExpiration);