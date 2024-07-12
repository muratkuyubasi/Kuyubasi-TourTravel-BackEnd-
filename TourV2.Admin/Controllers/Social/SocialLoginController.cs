using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SocialLoginController : BaseController
    {
        public IMediator _mediator { get; set; }

        public SocialLoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SocialLoginCommand userLoginCommand)
        {
            userLoginCommand.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _mediator.Send(userLoginCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            {
                result.Data.ProfilePhoto = $"Users/{result.Data.ProfilePhoto}";
            }
            return Ok(result.Data);
        }
    }
}
