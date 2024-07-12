using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Controllers.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TourCommentController : BaseController
    {
        public IMediator _mediator { get; set; }

        public TourCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get TourComment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTourCommentById")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> GetTourComment(Guid id)
        {
            var getQuery = new GetTourCommentQuery { Id = id };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get TourComment By TourMainId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTourCommentByMainId")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> GetTourCommentByMainId(int activeTourId)
        {
            var getQuery = new GetTourCommentByTourMainIdQuery { ActiveTourId = activeTourId };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }

        ///<summary>
        ///Get All TourComment
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllTourComments")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourCommentDto>))]
        public async Task<IActionResult> GetAllTourComments()
        {
            var query = new GetAllTourCommentQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        ///<summary>
        ///Get Popular TourComments
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllTourPopularComments")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourCommentDto>))]
        public async Task<IActionResult> GetAllTourPopularComments()
        {
            var query = new GetAllTourPopularCommentQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        ///<summary>
        ///New TourComment
        ///</summary>
        ///<param name="addTourCommentCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddTourComment")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> AddTourComment(AddTourCommentCommand addTourCommentCommand)
        {
            var response = await _mediator.Send(addTourCommentCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Confirm Comment
        ///</summary>
        ///<param name="confirmTourCommentCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("ConfirmTourComment")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> ConfirmTourComment(Guid Id)
        {
            var query = new ConfirmTourCommentCommand { Id = Id };

            var response = await _mediator.Send(query);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Confirm Comment
        ///</summary>
        ///<param name="confirmTourCommentCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("SelectPopularTourComment")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> SelectPopularTourComment(Guid Id)
        {
            var query = new SelectTourCommentCommand { Id = Id };

            var response = await _mediator.Send(query);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Delete TourComment
        ///</summary>
        ///<param name="deletePeriodCommand"></param>
        ///<returns></returns>
        ///
        [HttpDelete("DeleteTourComment")]
        [Produces("application/json", "application/xml", Type = typeof(TourCommentDto))]
        public async Task<IActionResult> DeleteTourComment(Guid Id)
        {
            var query = new DeleteTourCommentCommand
            {
                Id = Id
            };
            var response = await _mediator.Send(query);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }
    }
}
