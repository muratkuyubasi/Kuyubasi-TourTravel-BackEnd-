using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Controllers.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TourController : BaseController
    {
        private readonly IMediator _mediator;
        public TourController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create A Tour
        /// </summary>
        /// <param name="addTourCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(TourDto))]
        public async Task<IActionResult> AddTour(AddTourCommand addTourCommand)
        {
            var result = await _mediator.Send(addTourCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Update Exist Tour By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTourCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TourRecordDto))]
        public async Task<IActionResult> UpdateTour(Guid id, UpdateTourCommand updateTourCommand)
        {
            updateTourCommand.Code = id;
            var result = await _mediator.Send(updateTourCommand);
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTour/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TourDto))]
        public async Task<IActionResult> GetTour(int id)
        {
            var getTourQuery = new GetTourQuery { Id = id };

            var result = await _mediator.Send(getTourQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get GetTourRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTourRecord/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TourRecordDto))]
        public async Task<IActionResult> GetTourRecord(Guid id)
        {
            var getTourQuery = new GetTourRecordQuery { Id = id };

            var result = await _mediator.Send(getTourQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Categories BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTourByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourListDto>))]
        public async Task<IActionResult> GetAllTourByLang(string languageCode)
        {
            var getAllQuery = new GetAllToursQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Create A Tour Clicks
        /// </summary>
        /// <param name="addTourClickCommand"></param>
        /// <returns></returns>
        [HttpPost("AddTourClick")]
        [Produces("application/json", "application/xml", Type = typeof(TourClickDto))]
        public async Task<IActionResult> AddTourClick(AddTourClickCommand addTourClickCommand)
        {
            var result = await _mediator.Send(addTourClickCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Get BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTourClickCount")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourDto>))]
        public async Task<IActionResult> GetTourClickCount(int? activeTourId)
        {
            var getAllQuery = new GetTourClickCountQuery { ActiveTourId = activeTourId };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTour(int Id)
        {
            var deleteTourCommand = new DeleteTourCommand { Id = Id };
            var result = await _mediator.Send(deleteTourCommand);
            return ReturnFormattedResponse(result);
        }

    }
}
