using iTextSharp.text.pdf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using System.Text;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Handlers;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.Tour.Reservation;

namespace TourV2.Admin.Controllers.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ReservationController : BaseController
    {
        private readonly IMediator _mediator;
        private IWebHostEnvironment _webHostEnvironment;
        public ReservationController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Get Tour Reservation By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTourReservation/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourReservationDto>))]
        public async Task<IActionResult> GetTourReservation(int id)
        {
            var getQuery = new GetTourReservationQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);

        }

        /// <summary>
        /// Get Tour Reservation Person By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetTourReservationPerson/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationPersonDto))]
        public async Task<IActionResult> GetTourReservationPerson(int id)
        {
            var getQuery = new GetTourReservationPersonQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);

        }

        /// <summary>
        /// Get Reservation By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetReservation/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationDto))]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            var getQuery = new GetReservationQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);

        }
        
         /// <summary>
         /// Get Active Tour By Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("CreateCostCalculation/{id}")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CostCalculationDto))]
        public async Task<IActionResult> CreateCostCalculation(int id)
        {
            var getQuery = new CreateCostCalculationCommand
            {
                ActiveTourId = id
            };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Add Cost Row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("AddCostCalculation")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CostCalculationDto))]
        public async Task<IActionResult> AddCostCalculation(AddCostCalculationCommand request)
        {
            var result = await _mediator.Send(request);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Cost Row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("UpdateCostCalculation/{id}")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CostCalculationDto))]
        public async Task<IActionResult> UpdateCostCalculation(UpdateCostCalculationCommand request)
        {
            var result = await _mediator.Send(request);
            return ReturnFormattedResponse(result);
        }

        ///// <summary>
        ///// Update Cost All
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateAllCostCalculation/{id}")]
        //[Authorize]
        //[Produces("application/json", "application/xml", Type = typeof(CostCalculationDto))]
        //public async Task<IActionResult> UpdateAllCostCalculation(UpdateAllCostCalculationCommand request)
        //{
        //    var result = await _mediator.Send(request);
        //    return ReturnFormattedResponse(result);
        //}

        /// <summary>
        /// Get All Tours Reservation
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTourReservation")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourReservationDto>))]
        public async Task<IActionResult> GetAllTourReservation()
        {
            var getAllQuery = new GetAllTourReservationQuery { };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get All Tours Reservation Person
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTourReservationPerson")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourReservationPersonDto>))]
        public async Task<IActionResult> GetAllTourReservationPerson()
        {
            var getAllQuery = new GetAllTourReservationPersonQuery { };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }


        ///<summary>
        ///New Tour Reservation
        ///</summary>
        ///<param name="addTourReservationQuery"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddTourReservation")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationDto))]
        [AllowAnonymous]
        public async Task<IActionResult> AddTourReservation(AddTourReservationCommand addTourReservationQuery)
        {
            var response = await _mediator.Send(addTourReservationQuery);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }

            var result = await _mediator.Send(new GetInfoTourReservartionForPDFQuery() { Id = response.Data.Id });
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string loadPath = "";
            if (true)
                loadPath = _webHostEnvironment.WebRootPath + "/sablonPDF.pdf";
            else
                loadPath = _webHostEnvironment.WebRootPath + "/sablonPDF2.pdf";


            using (var reader = new PdfReader(loadPath))
            {
                using (var stream = new MemoryStream())
                {
                    using (var stamper = new PdfStamper(reader, stream))
                    {
                        AcroFields fields = stamper.AcroFields;
                        var regularFont = BaseFont.CreateFont(_webHostEnvironment.WebRootPath + "/calibri-font.ttf", "Cp1254", false);
                        fields.AddSubstitutionFont(regularFont);

                        fields.SetField("{{AdSoyad}}", result.Data.Fullname);
                        fields.SetField("{{Adres}}", result.Data.Address);
                        fields.SetField("{{FaturaNo}}", result.Data.BillNo);
                        fields.SetField("{{TurBilgi}}", result.Data.TourInfo);
                        fields.SetField("{{Tarih}}", result.Data.TicketDate);
                        fields.SetField("{{OdemeTarih}}", result.Data.PaymentDate);
                        fields.SetField("{{UrunKodu}}", result.Data.ProductCode);
                        fields.SetField("{{Musteriler}}", result.Data.ReservationPersons);
                        fields.SetField("{{EkstraUcret}}", result.Data.ExtraPrice);
                        fields.SetField("{{Adet}}", result.Data.Quantity);
                        fields.SetField("{{BirimFiyat}}", result.Data.QuantityPrice);
                        fields.SetField("{{BirimToplam}}", result.Data.QuantityTotalPrice);
                        fields.SetField("{{ToplamFiyat}}", result.Data.TotalPrice);


                        stamper.Writer.CloseStream = false;

                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;


                        using (var fs = new FileStream(_webHostEnvironment.WebRootPath + "/reservation/ticket_" + response.Data.Id + ".pdf", FileMode.OpenOrCreate))
                        {
                            stream.CopyTo(fs);
                        }

                        try
                        {
                            var attachmentList = new List<Attachment>();
                            attachmentList.Add(new Attachment(_webHostEnvironment.WebRootPath + "/reservation/ticket_" + response.Data.Id + ".pdf"));
                            var mailTemplate = await _mediator.Send(new GetEmailTemplateQuery
                            {
                                Id = Guid.Parse("bf40da5b-821f-4161-a25d-0487e12edab5")
                            });
                            EmailHelper.SendEmail(new SendEmailSpecification
                            {
                                Subject = mailTemplate.Data.Subject.Replace("##TurBilgi##", result.Data.TourInfo),
                                Body = mailTemplate.Data.Body.Replace("##AdSoyad##", result.Data.Fullname).Replace("##TurTarih##", result.Data.TicketDate).Replace("##TurAdi##", result.Data.TourName),
                                FromAddress = result.Data.DefaultSmtp.UserName,
                                Host = result.Data.DefaultSmtp.Host,
                                IsEnableSSL = result.Data.DefaultSmtp.IsEnableSSL,
                                Password = result.Data.DefaultSmtp.Password,
                                Port = result.Data.DefaultSmtp.Port,
                                ToAddress = response.Data.ContactEmail,
                                UserName = result.Data.DefaultSmtp.UserName,
                                isAttechment = true,
                                Attachments = attachmentList,
                                CCAddress = "rezervasyon@zsureisen.eu"
                            });
                        }
                        catch (Exception)
                        { }

                        //return File(stream.ToArray(), "application/pdf", fileDownloadName: "contract_" + Guid.NewGuid() + ".pdf");
                    }
                }
            }
            return Ok(response);

        }
        ///<summary>
        /// Update Tour Reservation
        ///</summary>
        ///<param name="updateTourReservationQuery"></param>
        ///<returns></returns>
        ///
        [HttpPut("UpdateTourReservation")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationDto))]
        public async Task<IActionResult> UpdateTourReservation(UpdateTourReservationCommand updateTourReservationQuery)
        {
            var response = await _mediator.Send(updateTourReservationQuery);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }


        ///<summary>
        /// Delete Tour Reservation
        ///</summary>
        ///<param name="Id"></param>
        ///<returns></returns>
        [HttpDelete("DeleteTourReservation/{id}")]
        public async Task<IActionResult> DeleteTourReservation(int id)
        {
            var deleteCommand = new DeleteTourReservationCommand { Id = id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }


        ///<summary>
        ///New Tour Reservation
        ///</summary>
        ///<param name="addTourReservationPersonQuery"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddTourReservationPerson")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationPersonDto))]
        public async Task<IActionResult> AddTourReservationPerson(AddTourReservationPersonCommand addTourReservationPersonQuery)
        {
            var response = await _mediator.Send(addTourReservationPersonQuery);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        /// Update Tour Reservation
        ///</summary>
        ///<param name="updateTourReservationPersonQuery"></param>
        ///<returns></returns>
        ///
        [HttpPut("UpdateTourReservationPerson")]
        [Produces("application/json", "application/xml", Type = typeof(TourReservationPersonDto))]
        public async Task<IActionResult> UpdateTourReservationPerson(UpdateTourReservationPersonCommand updateTourReservationPersonQuery)
        {
            var response = await _mediator.Send(updateTourReservationPersonQuery);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }


        ///<summary>
        /// Delete Tour Reservation
        ///</summary>
        ///<param name="Id"></param>
        ///<returns></returns>
        [HttpDelete("DeleteTourReservationPerson/{id}")]
        public async Task<IActionResult> DeleteTourReservationPerson(int id)
        {
            var deleteCommand = new DeleteTourReservationPersonCommand { Id = id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Active Tour By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Test/{id}")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(List<TourReservationDto>))]
        public async Task<IActionResult> Test(int id)
        {
            var getQuery = new CreateCostCalculationCommand
            {
                ActiveTourId = id
            };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }


        /// <summary>
        /// Get All Tours Reservation
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTourReservationTourId")]
        [Produces("application/json", "application/xml", Type = typeof(List<TourReservationDto>))]
        public async Task<IActionResult> GetAllTourReservationTourId(int id)
        {
            var getAllQuery = new GetAllParticipantByTourIdQuery {TourId = id };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }

    }
}
