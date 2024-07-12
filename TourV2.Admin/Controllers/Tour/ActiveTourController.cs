using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Controllers.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ActiveTourController : BaseController
    {
        private readonly IMediator _mediator;
        private IWebHostEnvironment _webHostEnvironment;
        public ActiveTourController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        #region ACTIVE TOUR
        /// <summary>
        /// Create A ActiveTour
        /// </summary>
        /// <param name="addActiveTourCommand"></param>
        /// <returns></returns>
        [HttpPost("AddTour")]
        [Produces("application/json", "application/xml", Type = typeof(ActiveTourDto))]
        public async Task<IActionResult> AddTour(AddActiveTourCommand addActiveTourCommand)
        {
            var result = await _mediator.Send(addActiveTourCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            else
            {
                var tourCategory = new AddActiveTourCategoryCommand
                {
                    ActiveTourId = result.Data.Id,
                    CategoryRecordId = addActiveTourCommand.TourCategories[0].CategoryRecordId
                };
                var categoryResult = await _mediator.Send(tourCategory);

                var tourTransportation = new AddActiveTourTransportationCommand
                {
                    ActiveTourId = result.Data.Id,
                    Title = addActiveTourCommand.TourTransportation.Title
                };
                var transportationResult = await _mediator.Send(tourTransportation);

            }

            return Ok(result);
        }

        [HttpPut("UpdateTour/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ActiveTourDto))]
        public async Task<IActionResult> UpdateTour(Guid id, UpdateActiveTourCommand updateTourCommand)
        {
            updateTourCommand.Code = id;
            var result = await _mediator.Send(updateTourCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            else
            {
                var tourCategory = new UpdateActiveTourCategoryCommand
                {
                    Id = updateTourCommand.TourCategories[0].Id,
                    ActiveTourId = result.Data.Id,
                    CategoryRecordId = updateTourCommand.TourCategories[0].CategoryRecordId
                };
                var categoryResult = await _mediator.Send(tourCategory);

                var tourTransportation = new UpdateActiveTourTransportationCommand
                {
                    Id = updateTourCommand.TourTransportation.Id,
                    ActiveTourId = result.Data.Id,
                    Title = updateTourCommand.TourTransportation.Title
                };
                var transportationResult = await _mediator.Send(tourTransportation);

            }
            return Ok(result);
        }
        /// <summary>
        /// Get GetRecord By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>



        /// <summary>
        /// Get Tour BY Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetActiveTour/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ActiveTourDto))]
        public async Task<IActionResult> GetActiveTour(int id)
        {
            var getTourQuery = new GetActiveTourQuery { Id = id };
            var result = await _mediator.Send(getTourQuery);
            return ReturnFormattedResponse(result);
        }

        [HttpGet("GetAllTourByLang/{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<ActiveTourDto>))]
        public async Task<IActionResult> GetAllTourByLang(string languageCode)
        {
            var getAllQuery = new GetAllActiveToursQuery { LanguageCode = languageCode };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }

        [HttpGet("GetAllTourSearchByLang")]
        [Produces("application/json", "application/xml", Type = typeof(List<ActiveTourDto>))]
        public async Task<IActionResult> GetAllTourSearchByLang([FromQuery] GetAllActiveToursSearchQuery getAllQuery)
        {
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }

        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTour/{Id}")]
        public async Task<IActionResult> DeleteTour(int Id)
        {
            var deleteTourCommand = new DeleteActiveTourCommand { Id = Id };
            var result = await _mediator.Send(deleteTourCommand);
            return ReturnFormattedResponse(result);
        }


        #endregion

        #region PRICE
        /// <summary>
        /// Add Price
        /// </summary>
        /// <param name="addPriceCommand"></param>
        /// <returns></returns>
        [HttpPost("AddPrice")]
        [Produces("application/json", "application/xml", Type = typeof(TourPrice))]
        public async Task<IActionResult> AddPrice(AddActiveTourPriceCommand addPriceCommand)
        {
            var result = await _mediator.Send(addPriceCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }


        [HttpPut("UpdatePrice")]
        [Produces("application/json", "application/xml", Type = typeof(TourPrice))]
        public async Task<IActionResult> UpdatePrice(UpdateActiveTourPriceCommand updatePriceCommand)
        {
            var result = await _mediator.Send(updatePriceCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeletePrice/{Id}")]
        public async Task<IActionResult> DeletePrice(int Id)
        {
            var deleteCommand = new DeleteActiveTourPriceCommand { Id = Id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }

        #endregion

        #region DAY
        /// <summary>
        /// Add Day
        /// </summary>
        /// <param name="addDayCommand"></param>
        /// <returns></returns>
        [HttpPost("AddDay")]
        [Produces("application/json", "application/xml", Type = typeof(TourDay))]
        public async Task<IActionResult> AddDay(AddActiveTourDayCommand addDayCommand)
        {
            var result = await _mediator.Send(addDayCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }
       
        [HttpPut("UpdateDay")]
        [Produces("application/json", "application/xml", Type = typeof(TourPrice))]
        public async Task<IActionResult> UpdateDay(UpdateActiveTourDayCommand updateCommand)
        {
            var result = await _mediator.Send(updateCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDay/{Id}")]
        public async Task<IActionResult> DeleteDay(int Id)
        {
            var deleteCommand = new DeleteActiveTourDayCommand { Id = Id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }

        #endregion

        #region SPECIFICATION
        /// <summary>
        /// Add Specification
        /// </summary>
        /// <param name="addDayCommand"></param>
        /// <returns></returns>
        [HttpPost("AddSpecification")]
        [Produces("application/json", "application/xml", Type = typeof(TourSpecification))]
        public async Task<IActionResult> AddSpecification(AddActiveTourSpecificationCommand addSpecificationCommand)
        {
            var result = await _mediator.Send(addSpecificationCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateSpecification")]
        [Produces("application/json", "application/xml", Type = typeof(TourSpecification))]
        public async Task<IActionResult> UpdateSpecification(UpdateActiveTourSpecificationCommand updateCommand)
        {
            var result = await _mediator.Send(updateCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteSpecification/{Id}")]
        public async Task<IActionResult> DeleteSpecification(int Id)
        {
            var deleteCommand = new DeleteActiveTourSpecificationCommand { Id = Id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }

        #endregion

        #region DEPARTURE
        /// <summary>
        /// Add Departure
        /// </summary>
        /// <param name="addDepartureCommand"></param>
        /// <returns></returns>
        [HttpPost("AddDeparture")]
        [Produces("application/json", "application/xml", Type = typeof(TourDeparture))]
        public async Task<IActionResult> AddDeparture(AddActiveTourDepartureCommand addDepartureCommand)
        {
            var result = await _mediator.Send(addDepartureCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateDeparture")]
        [Produces("application/json", "application/xml", Type = typeof(TourDeparture))]
        public async Task<IActionResult> UpdateDeparture(UpdateActiveTourDepartureCommand updateCommand)
        {
            var result = await _mediator.Send(updateCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDeparture/{Id}")]
        public async Task<IActionResult> DeleteDeparture(int Id)
        {
            var deleteCommand = new DeleteActiveTourDepartureCommand { Id = Id };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }

        [HttpGet("GetAllTourDepartureByLang/{tourId}")]
        [Produces("application/json", "application/xml", Type = typeof(List<DepartureListDto>))]
        public async Task<IActionResult> GetAllTourDepartureByLang(int tourId)
        {
            var getAllQuery = new GetAllTourDepartureQuery { ActiveTourId = tourId };
            var result = await _mediator.Send(getAllQuery);
            return Ok(result);
        }

        #endregion


        #region MEDIA
        /// <summary>
        /// Add Media
        /// </summary>
        /// <param name="addDepartureCommand"></param>
        /// <returns></returns>
        [HttpPost("AddMedia")]
        [Produces("application/json", "application/xml", Type = typeof(TourMedia))]
        public async Task<IActionResult> AddMedia()
        {
            var addMediaCommand = new AddActiveTourMediaCommand()
            {
                FormFile = Request.Form.Files,
                RootPath = _webHostEnvironment.WebRootPath
            };
            //addMediaCommand.FormFile = Request.Form.Files;
            //addMediaCommand.RootPath = _webHostEnvironment.WebRootPath;
                
            var result = await _mediator.Send(addMediaCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Delete Tour By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("DeleteMedia/{Id}")]
        public async Task<IActionResult> DeleteMedia(int Id)
        {
            var deleteCommand = new DeleteActiveTourMediaCommand { 
                Id = Id, 
                RootPath = _webHostEnvironment.WebRootPath 
            };
            var result = await _mediator.Send(deleteCommand);
            return ReturnFormattedResponse(result);
        }
        #endregion
    }
}
