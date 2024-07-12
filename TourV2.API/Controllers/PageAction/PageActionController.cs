using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourV2.API.Controllers
{
    /// <summary>
    /// Page Action
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PageActionController : BaseController
    {
        public IMediator _mediator { get; set; }
        /// <summary>
        /// Page Action
        /// </summary>
        /// <param name="mediator"></param>
        public PageActionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get All Page Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("PageActions")]
        [Produces("application/json", "application/xml", Type = typeof(List<PageActionDto>))]
        public async Task<IActionResult> GetPageActions()
        {
            var getAllPageActionQuery = new GetAllPageActionQuery { };
            var result = await _mediator.Send(getAllPageActionQuery);
            return Ok(result);
        }
        /// <summary>
        /// Add Page Action
        /// </summary>
        /// <param name="addPageActionCommand"></param>
        /// <returns></returns>
        [HttpPost("PageAction")]
        [Produces("application/json", "application/xml", Type = typeof(PageActionDto))]
        public async Task<IActionResult> AddPageAction(AddPageActionCommand addPageActionCommand)
        {
            var result = await _mediator.Send(addPageActionCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Page Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("PageAction/{Id}")]
        public async Task<IActionResult> DeletePageAction(Guid Id)
        {
            var deletePageActionCommand = new DeletePageActionCommand { Id = Id };
            var result = await _mediator.Send(deletePageActionCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
