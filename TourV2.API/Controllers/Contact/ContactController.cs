using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourV2.API.Controllers;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;

namespace TourV2.WEBAPI.Controllers.Contact
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactController : BaseController
    {
        public IMediator _mediator { get; set; }

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetContactMessageById")]
        [Produces("application/json", "application/xml", Type = typeof(ContactMessageDto))]
        public async Task<IActionResult> GetContactMessage(Guid id)
        {
            var getQuery = new GetContactMessageQuery { Id = id };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get NewsletterSubscriptions By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetNewsletterSubscriptionById")]
        [Produces("application/json", "application/xml", Type = typeof(NewsletterSubscriptionDto))]
        public async Task<IActionResult> GetNewsletterSubscription(Guid id)
        {
            var getQuery = new GetNewsletterSubscriptionQuery { Id = id };

            var result = await _mediator.Send(getQuery);
            return ReturnFormattedResponse(result);
        }

        ///<summary>
        ///Get All ContactMessage
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllContactMessages")]
        [Produces("application/json", "application/xml", Type = typeof(List<ContactMessageDto>))]
        public async Task<IActionResult> GetAllContactMessages()
        {
            var query = new GetAllContactMessageQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        ///<summary>
        ///Get All NewsletterSubscriptions
        ///</summary>
        ///<returns></returns>

        [HttpGet("GetAllNewsletterSubscriptions")]
        [Produces("application/json", "application/xml", Type = typeof(List<NewsletterSubscriptionDto>))]
        public async Task<IActionResult> GetAllNewsletterSubscriptions()
        {
            var query = new GetAllNewsletterSubscriptionQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        ///<summary>
        ///New NewsletterSubscription
        ///</summary>
        ///<param name="addNewsletterSubscriptionCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddNewsletterSubscription")]
        [Produces("application/json", "application/xml", Type = typeof(NewsletterSubscriptionDto))]
        public async Task<IActionResult> AddNewsletterSubscription(AddNewsletterSubscriptionCommand addNewsletterSubscriptionCommand)
        {
            var response = await _mediator.Send(addNewsletterSubscriptionCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }
        ///<summary>
        ///New ContactMessage
        ///</summary>
        ///<param name="addContactMessageCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("AddContactMessage")]
        [Produces("application/json", "application/xml", Type = typeof(ContactMessageDto))]
        public async Task<IActionResult> AddContactMessage(AddContactMessageCommand addContactMessageCommand)
        {
            var response = await _mediator.Send(addContactMessageCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }

        ///<summary>
        ///Delete ContactMessage
        ///</summary>
        ///<param name="deletePeriodCommand"></param>
        ///<returns></returns>
        ///
        [HttpPost("DeleteContactMessage")]
        [Produces("application/json", "application/xml", Type = typeof(ContactMessageDto))]
        public async Task<IActionResult> DeleteContactMessage(DeleteContactMessageCommand deleteContactMessageCommand)
        {
            var response = await _mediator.Send(deleteContactMessageCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }///<summary>
         ///Delete NewsletterSubscription
         ///</summary>
         ///<param name="deleteNewsletterSubscriptionCommand"></param>
         ///<returns></returns>
         ///
        [HttpPost("DeleteNewsletterSubscription")]
        [Produces("application/json", "application/xml", Type = typeof(NewsletterSubscriptionDto))]
        public async Task<IActionResult> DeleteNewsletterSubscription(DeleteNewsletterSubscriptionCommand deleteNewsletterSubscriptionCommand)
        {
            var response = await _mediator.Send(deleteNewsletterSubscriptionCommand);

            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return Ok(response);

        }
    }
}
