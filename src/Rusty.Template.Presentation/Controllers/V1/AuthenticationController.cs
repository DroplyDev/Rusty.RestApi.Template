using Microsoft.AspNetCore.Mvc;

namespace Rusty.Template.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class AuthenticationController : BaseApiController
{
    public IActionResult Login()
    {
        return Ok();
    }
}