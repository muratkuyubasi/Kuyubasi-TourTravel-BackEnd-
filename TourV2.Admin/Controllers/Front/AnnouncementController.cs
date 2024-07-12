using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourV2.Admin.Controllers
{
    /// <summary>
    /// FrontAnnouncement
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FrontAnnouncementController : BaseController
    {
        public IMediator _mediator { get; set; }
        private IWebHostEnvironment _webHostEnvironment;
        /// <summary>
        /// FrontAnnouncement
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public FrontAnnouncementController(
            IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Create A FrontAnnouncement
        /// </summary>
        /// <param name="addFrontAnnouncementCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementDto))]
        public async Task<IActionResult> AddFrontAnnouncement(AddFrontAnnouncementCommand addFrontAnnouncementCommand)
        {
            var result = await _mediator.Send(addFrontAnnouncementCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetFrontAnnouncement", new { id = result.Data.Id }, result.Data);
        }
        /// <summary>
        /// Update Exist FrontAnnouncement By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFrontAnnouncementCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementDto))]
        public async Task<IActionResult> UpdateFrontAnnouncement(int id, UpdateFrontAnnouncementCommand updateFrontAnnouncementCommand)
        {
            updateFrontAnnouncementCommand.Id = id;
            var result = await _mediator.Send(updateFrontAnnouncementCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get FrontAnnouncement By Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetFrontAnnouncement")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementDto))]
        public async Task<IActionResult> GetFrontAnnouncement(Guid code)
        {
            var getFrontAnnouncementQuery = new GetFrontAnnouncementQuery { Code = code };

            var result = await _mediator.Send(getFrontAnnouncementQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All FrontAnnouncements
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetFrontAnnouncements")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontAnnouncementDto>))]
        public async Task<IActionResult> GetFrontAnnouncements()
        {
            var getAllFrontAnnouncementQuery = new GetAllFrontAnnouncementQuery { };
            var result = await _mediator.Send(getAllFrontAnnouncementQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete FrontAnnouncement By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFrontAnnouncement(Guid code)
        {
            var deleteFrontAnnouncementCommand = new DeleteFrontAnnouncementCommand { Code = code };
            var result = await _mediator.Send(deleteFrontAnnouncementCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create A FrontAnnouncementRecord
        /// </summary>
        /// <param name="addFrontAnnouncementRecordCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementRecordDto))]
        public async Task<IActionResult> AddFrontAnnouncementRecord(AddFrontAnnouncementRecordCommand addFrontAnnouncementRecordCommand)
        {
            addFrontAnnouncementRecordCommand.RootPath = _webHostEnvironment.WebRootPath;
            var result = await _mediator.Send(addFrontAnnouncementRecordCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetFrontAnnouncementRecord", new { id = result.Data.Id }, result.Data);
        }
        /// <summary>
        /// Update Exist FrontAnnouncementRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFrontAnnouncementRecordCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementRecordDto))]
        public async Task<IActionResult> UpdateFrontAnnouncementRecord(int id, UpdateFrontAnnouncementRecordCommand updateFrontAnnouncementRecordCommand)
        {
            updateFrontAnnouncementRecordCommand.Id = id;
            var result = await _mediator.Send(updateFrontAnnouncementRecordCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get FrontAnnouncementRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetFrontAnnouncementRecord")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementRecordDto))]
        public async Task<IActionResult> GetFrontAnnouncementRecord(int id)
        {
            var getFrontAnnouncementRecordQuery = new GetFrontAnnouncementRecordQuery { Id = id };

            var result = await _mediator.Send(getFrontAnnouncementRecordQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All FrontAnnouncementRecords
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetFrontAnnouncementRecords")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontAnnouncementRecordDto>))]
        public async Task<IActionResult> GetFrontAnnouncementRecords()
        {
            var getAllFrontAnnouncementRecordQuery = new GetAllFrontAnnouncementRecordQuery { };
            var result = await _mediator.Send(getAllFrontAnnouncementRecordQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete FrontAnnouncementRecord By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFrontAnnouncementRecord(int Id)
        {
            var deleteFrontAnnouncementRecordCommand = new DeleteFrontAnnouncementRecordCommand { Id = Id };
            var result = await _mediator.Send(deleteFrontAnnouncementRecordCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
