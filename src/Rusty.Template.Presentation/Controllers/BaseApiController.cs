#region

using Microsoft.AspNetCore.Mvc;
using Rusty.Template.Contracts.Responses;
using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Presentation.Controllers;

/// <summary>
/// Base api controller
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ApiExceptionResponse))]
public abstract class BaseApiController : ControllerBase
{
}