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
    public class RegionController : BaseController
    {
        private readonly IMediator _mediator;
        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create A Region
        /// </summary>
        /// <param name="addRegionCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(RegionDto))]
        public async Task<IActionResult> AddRegion(AddRegionCommand addRegionCommand)
        {
            var result = await _mediator.Send(addRegionCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Update Exist Region By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRegionCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(RegionRecordDto))]
        public async Task<IActionResult> UpdateRegion(Guid id, UpdateRegionCommand updateRegionCommand)
        {
            updateRegionCommand.Code = id;
            var result = await _mediator.Send(updateRegionCommand);
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetRegion/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(RegionDto))]
        public async Task<IActionResult> GetRegion(int id)
        {
            var getRegionQuery = new GetRegionQuery { Id = id };

            var result = await _mediator.Send(getRegionQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get GetRegionRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetRegionRecord/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(RegionRecordDto))]
        public async Task<IActionResult> GetRegionRecord(Guid id)
        {
            var getRegionQuery = new GetRegionRecordQuery { Id = id };

            var result = await _mediator.Send(getRegionQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Categories BY Lang
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRegionByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<RegionListDto>))]
        public async Task<IActionResult> GetAllRegionByLang(string languageCode)
        {
            var getAllQuery = new GetAllRegionsQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Region By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRegion(int Id)
        {
            var deleteRegionCommand = new DeleteRegionCommand { Id = Id };
            var result = await _mediator.Send(deleteRegionCommand);
            return ReturnFormattedResponse(result);
        }



    }
}
