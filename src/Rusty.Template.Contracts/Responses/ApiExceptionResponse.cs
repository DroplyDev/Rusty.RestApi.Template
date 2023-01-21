#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

[SwaggerSchema("Api exception response model")]
public sealed record ApiExceptionResponse([SwaggerSchema("Exception title")] string Title,
										  [SwaggerSchema("Exception status code")]
										  int Status)
{
}