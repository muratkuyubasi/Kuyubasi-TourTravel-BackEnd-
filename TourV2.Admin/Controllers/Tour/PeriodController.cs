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
    public class PeriodController : BaseController
    {
        private readonly IMediator _mediator;
        public PeriodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create A Period
        /// </summary>
        /// <param name="addPeriodCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(PeriodDto))]
        public async Task<IActionResult> AddPeriod(AddPeriodCommand addPeriodCommand)
        {
            var result = await _mediator.Send(addPeriodCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Update Exist Period By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePeriodCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PeriodRecordDto))]
        public async Task<IActionResult> UpdatePeriod(Guid id, UpdatePeriodCommand updatePeriodCommand)
        {
            updatePeriodCommand.Code = id;
            var result = await _mediator.Send(updatePeriodCommand);
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetPeriod/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PeriodDto))]
        public async Task<IActionResult> GetPeriod(int id)
        {
            var getPeriodQuery = new GetPeriodQuery { Id = id };

            var result = await _mediator.Send(getPeriodQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get GetPeriodRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetPeriodRecord/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(PeriodRecordDto))]
        public async Task<IActionResult> GetPeriodRecord(Guid id)
        {
            var getPeriodQuery = new GetPeriodRecordQuery { Id = id };

            var result = await _mediator.Send(getPeriodQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Categories BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPeriodByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<PeriodListDto>))]
        public async Task<IActionResult> GetAllPeriodByLang(string languageCode)
        {
            var getAllQuery = new GetAllPeriodsQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Period By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePeriod(int Id)
        {
            var deletePeriodCommand = new DeletePeriodCommand { Id = Id };
            var result = await _mediator.Send(deletePeriodCommand);
            return ReturnFormattedResponse(result);
        }



    }
}
