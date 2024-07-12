using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.API.Controllers.EmailTemplate
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EmailTemplateController : BaseController
    {
        public IMediator _mediator { get; set; }
        private readonly ILogger<EmailTemplateController> _logger;
        /// <summary>
        /// Role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public EmailTemplateController(
            IMediator mediator,
            ILogger<EmailTemplateController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Create  Email Template
        /// </summary>
        /// <param name="addEmailTemplateCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EmailTemplateDto))]
        public async Task<IActionResult> AddEmailTemplate(AddEmailTemplateCommand addEmailTemplateCommand)
        {
            var result = await _mediator.Send(addEmailTemplateCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetEmailTemplate", new { id = result.Data.Id }, result.Data);
        }
        /// <summary>
        /// Update Exist AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmailTemplateCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> UpdateAppSetting(Guid id, UpdateEmailTemplateCommand updateEmailTemplateCommand)
        {
            updateEmailTemplateCommand.Id = id;
            var result = await _mediator.Send(updateEmailTemplateCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get Email Template By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetEmailTemplate")]
        [Produces("application/json", "application/xml", Type = typeof(EmailTemplateDto))]
        public async Task<IActionResult> GetEmailTemplate(Guid id)
        {
            _logger.LogTrace("GetAppSetting");
            var getEmailTemplateQuery = new GetEmailTemplateQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getEmailTemplateQuery);
            return ReturnFormattedResponse(result);

        }
        /// <summary>
        /// Get All Email Templates
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetEmailTemplates")]
        [Produces("application/json", "application/xml", Type = typeof(List<EmailTemplateDto>))]
        public async Task<IActionResult> GetEmailTemplates()
        {
            var getAllEmailTemplateQuery = new GetAllEmailTemplateQuery
            {
            };
            var result = await _mediator.Send(getAllEmailTemplateQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Delete Email Template By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DelterEmailTemplate(Guid Id)
        {
            var deleteEmailTemplateCommand = new DeleteEmailTemplateCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(deleteEmailTemplateCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
