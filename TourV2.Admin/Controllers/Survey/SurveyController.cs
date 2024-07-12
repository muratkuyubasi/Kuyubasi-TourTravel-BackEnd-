using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourV2.Repository.PeriodEducation;
using TourV2.Repository;
using TourV2.Data.Dto.Tour;
using TourV2.MediatR.Commands.Survery;

namespace TourV2.Admin.Controllers.Survey
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : BaseController
    {

        public IMediator _mediator { get; set; }
        public IEmailSMTPSettingRepository _email { get; set; }
        private IWebHostEnvironment _webHostEnvironment;
        private IPeriodEducationRepository _period;
        private IMapper _mapper;
        public SurveyController(IMediator mediator, IEmailSMTPSettingRepository email, IWebHostEnvironment webHostEnvironment, IPeriodEducationRepository period, IMapper mapper)
        {
            _mediator = mediator;
            _email = email;
            _webHostEnvironment = webHostEnvironment;
            _period = period;
            _mapper = mapper;
        }
        ///<summary>
        ///New Survey
        ///</summary>
        ///<param name="addSurveyCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddSurvey")]
        [Produces("application/json", "application/xml", Type = typeof(SurveyDto))]
        public async Task<IActionResult> AddSurvey(AddSurveryCommand surveryCommand)
        {
            var response = await _mediator.Send(surveryCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

    }
}

