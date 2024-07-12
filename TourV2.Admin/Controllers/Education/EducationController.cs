using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Data.Entities;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Commands.EducaitonForm;
using TourV2.MediatR.Commands.MosqueRepository;
using TourV2.MediatR.Commands.PeriodEducation;
using TourV2.MediatR.Commands.State;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.EducationForm;
using TourV2.MediatR.Queries.Mosque;
using TourV2.MediatR.Queries.PeriodEducation;
using TourV2.MediatR.Queries.State;
using TourV2.Repository;
using TourV2.Repository.PeriodEducation;

namespace TourV2.Admin.Controllers.Education
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : BaseController
    {
        public IMediator _mediator { get; set; }
        public IEmailSMTPSettingRepository _email { get; set; }
        private IWebHostEnvironment _webHostEnvironment;
        private IPeriodEducationRepository _period;
        private IMapper _mapper;
        public EducationController(IMediator mediator, IEmailSMTPSettingRepository email, IWebHostEnvironment webHostEnvironment, IPeriodEducationRepository period, IMapper mapper)
        {
            _mediator = mediator;
            _email = email;
            _webHostEnvironment = webHostEnvironment;
            _period = period;
            _mapper = mapper;
        }

        ///<summary>
        ///New EducationForm
        ///</summary>
        ///<param name="addEducationFormCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddEducationForm")]
        [Produces("application/json", "application/xml", Type = typeof(EducationFormDTO))]
        public async Task<IActionResult> AddEducation(AddEducationFormCommand addEducationFormCommand)
        {
            var response = await _mediator.Send(addEducationFormCommand);
            var result = await _email.All.FirstOrDefaultAsync();
            var attachmentList = new List<Attachment>();

            var mailTemplate = await _mediator.Send(new GetEmailTemplateQuery

            {
                Id = Guid.Parse("bf40da5b-821f-4161-a25d-0487e12edab5")
            });
            string personType = "";
            switch (addEducationFormCommand.PersonType)
            {
                case 1: personType = "Öğrencinin annesiyim"; break;
                case 2: personType = "Öğrencinin babasıyım"; break;
                case 3: personType = "Cami din görevlisiyim"; break;
                case 4: personType = "Diğer"; break;
            }
            var message = $@"<p>Değerli {addEducationFormCommand.StudentNameSurname},<br>  Erdemli Gençlik Değerler Eğitimi programına başvurunuz alınmıştır. İlginiz için teşekkür ederiz.<br> Uçuş ve otel işlemleri için planlamalar yapıldığında sizi bilgilendireceğiz.<br>İyi günler dileriz.<br>Zsu Reisen </p>
            <div style='display: grid'>
                <table style='border-collapse: collapse; width: 100%;'>
<tr>
                        <td style='border: 1px solid black; padding: 8px; color: red;'>Kayıt Formunu Dolduran Kişi</td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px; color: black;'>{personType} </td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px; color: red;'>Öğrencinin Adı Soyadı</td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px; color: black;'> {addEducationFormCommand.StudentNameSurname}</td>
                    </tr>
<tr>
                        <td style='border: 1px solid black; padding: 8px; color: red;'>Öğrencinin Maili</td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px;'>{addEducationFormCommand.StudentMail}</td>
                    </tr>
<tr>
                        <td style='border: 1px solid black; padding: 8px; color: red;'>Öğrencinin Doğum Tarihi</td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px; color: black;'> {addEducationFormCommand.Studentbirthdate}</td>
                    </tr>
   
<tr>
                        <td style='border: 1px solid black; padding: 8px; color: red;'>Öğrencinin adresi</td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid black; padding: 8px; color: black;'> {addEducationFormCommand.StudentAddress}</td>
                    </tr>

                     
                    
                </table>


            </div>";

            attachmentList.Add(new Attachment(_webHostEnvironment.WebRootPath + addEducationFormCommand.PasaportPath));
            attachmentList.Add(new Attachment(_webHostEnvironment.WebRootPath + addEducationFormCommand.IdentificationPath));
            attachmentList.Add(new Attachment(_webHostEnvironment.WebRootPath + addEducationFormCommand.ReceiptPath));

            try
            {
                //EmailHelper.SendMailByMailKit(

                // message,
                //"Erdemli Gençlik Formu",
                //addEducationFormCommand.StudentMail);

                EmailHelper.SendEmail(new SendEmailSpecification


                {
                    Subject = "Erdemli Gençlik Formu",
                    Body = message,
                    FromAddress = result.UserName,
                    Host = result.Host,
                    IsEnableSSL = result.IsEnableSSL,
                    Password = result.Password,
                    Port = result.Port,
                    ToAddress = addEducationFormCommand.StudentMail,
                    UserName = result.UserName,
                    isAttechment = true,
                    Attachments = attachmentList,
                    CCAddress = "rezervasyon@zsureisen.eu"
                });
            }
            catch (Exception)
            {
            }

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }


        ///<summary>
        ///Get All EducationForm
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllEducationForms")]
        [Produces("application/json", "application/xml", Type = typeof(List<EducationFormDTO>))]
        public async Task<IActionResult> GetAllContactMessages()
        {
            var query = new GetAllEducationFormQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        ///<summary>
        ///New State
        ///</summary>
        ///<param name="addEducationFormCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddState")]
        [Produces("application/json", "application/xml", Type = typeof(StateDto))]
        public async Task<IActionResult> AddState(AddStateCommand addStateCommand)
        {
            var response = await _mediator.Send(addStateCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);
        }


        ///<summary>
        ///Get All State
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllState")]
        [Produces("application/json", "application/xml", Type = typeof(List<StateDto>))]
        public async Task<IActionResult> GetAllState()
        {
            var query = new GetAllStateQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        ///<summary>
        ///New Mosque
        ///</summary>
        ///<param name="addMosqueCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddMosque")]
        [Produces("application/json", "application/xml", Type = typeof(MosqueDto))]
        public async Task<IActionResult> AddMosque(AddMosqueCommand mosqueCommand)
        {
            var response = await _mediator.Send(mosqueCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Get All Mosque
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllMosque")]
        [Produces("application/json", "application/xml", Type = typeof(List<MosqueDto>))]
        public async Task<IActionResult> GetAllMosque()
        {
            var query = new GetAllMosqueQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        ///<summary>
        ///Get All MosqueByStateId
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllMosque/{stateId}")]
        [Produces("application/json", "application/xml", Type = typeof(List<MosqueDto>))]
        public async Task<IActionResult> GetAllMosqueByStateId(int stateId)
        {
            var query = new GetAllMosqueByStateIdQuery { StateId = stateId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        ///<summary>
        ///New PeriodEducation
        ///</summary>
        ///<param name="addPeriodEducationCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddPeriodEducation")]
        [Produces("application/json", "application/xml", Type = typeof(PeriodEducationDto))]
        public async Task<IActionResult> AddPeriodEducation(AddPeriodEducationCommand periodEducationCommand)
        {
            var response = await _mediator.Send(periodEducationCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Get All PeriodEducation
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllPeriodEducation")]
        [Produces("application/json", "application/xml", Type = typeof(List<PeriodEducationDto>))]
        public async Task<IActionResult> GetAllPeriodEducation()
        {
            var query = new GetAllPeriodEducationQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Get PeriodEducation By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetPeriodEducationById")]
        [Produces("application/json", "application/xml", Type = typeof(PeriodEducationDto))]
        public async Task<IActionResult> GetPeriodEducation(int id)
        {
            var getQuery = new GetPeriodEducationQuery { Id = id };
            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }


        ///<summary>
        ///Get All MosqueByStateId
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllEducationFormByPeriodId/{periodId}")]
        [Produces("application/json", "application/xml", Type = typeof(List<EducationFormDTO>))]
        public async Task<IActionResult> GetAllEducationFormByPeriodId(int periodId)
        {
            var query = new GetAllEducationFormByPeriodIdQuery { PeriodId = periodId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }




}