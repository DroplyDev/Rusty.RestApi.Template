#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace Rusty.Template.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
// [Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
}