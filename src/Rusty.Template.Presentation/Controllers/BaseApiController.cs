using Microsoft.AspNetCore.Mvc;

namespace Rusty.Template.Presentation.Controllers;

/// <summary>
///     The base api controller class
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{

}