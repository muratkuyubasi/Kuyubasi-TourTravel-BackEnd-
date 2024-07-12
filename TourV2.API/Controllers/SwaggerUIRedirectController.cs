using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourV2.API.Controllers
{
    [Route("")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SwaggerUIRedirectController : ControllerBase
    {
        [Route(""), HttpGet]
        [AllowAnonymous]
        public IActionResult Swagger()
        {
            return Redirect("~/swagger");
        }
    }
}
