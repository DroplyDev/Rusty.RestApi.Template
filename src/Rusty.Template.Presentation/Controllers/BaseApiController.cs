using Microsoft.AspNetCore.Mvc;

namespace Rusty.Template.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
}