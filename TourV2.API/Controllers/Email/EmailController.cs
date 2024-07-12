using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourV2.MediatR.Commands;

namespace TourV2.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EmailController : BaseController
    {
        IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="sendEmailCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendEmail")]
        [Produces("application/json", "application/xml", Type = typeof(void))]
        public async Task<IActionResult> SendEmail(SendEmailCommand sendEmailCommand)
        {
            var result = await _mediator.Send(sendEmailCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
