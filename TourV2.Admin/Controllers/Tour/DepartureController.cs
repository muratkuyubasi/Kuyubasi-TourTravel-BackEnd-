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
    public class DepartureController : BaseController
    {
        private readonly IMediator _mediator;
        public DepartureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create A Departure
        /// </summary>
        /// <param name="addDepartureCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(DepartureDto))]
        public async Task<IActionResult> AddDeparture(AddDepartureCommand addDepartureCommand)
        {
            var result = await _mediator.Send(addDepartureCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Update Exist Departure By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDepartureCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(DepartureRecordDto))]
        public async Task<IActionResult> UpdateDeparture(Guid id, UpdateDepartureCommand updateDepartureCommand)
        {
            updateDepartureCommand.Code = id;
            var result = await _mediator.Send(updateDepartureCommand);
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetDeparture/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(DepartureDto))]
        public async Task<IActionResult> GetDeparture(int id)
        {
            var getDepartureQuery = new GetDepartureQuery { Id = id };

            var result = await _mediator.Send(getDepartureQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get GetDepartureRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetDepartureRecord/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(DepartureRecordDto))]
        public async Task<IActionResult> GetDepartureRecord(Guid id)
        {
            var getDepartureQuery = new GetDepartureRecordQuery { Id = id };

            var result = await _mediator.Send(getDepartureQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Categories BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDepartureByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<DepartureListDto>))]
        public async Task<IActionResult> GetAllDepartureByLang(string languageCode)
        {
            var getAllQuery = new GetAllDeparturesQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Departure By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDeparture(int Id)
        {
            var deleteDepartureCommand = new DeleteDepartureCommand { Id = Id };
            var result = await _mediator.Send(deleteDepartureCommand);
            return ReturnFormattedResponse(result);
        }



    }
}
