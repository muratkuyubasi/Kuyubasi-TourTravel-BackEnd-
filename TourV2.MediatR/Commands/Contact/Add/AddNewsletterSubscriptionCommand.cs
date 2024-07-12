using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddNewsletterSubscriptionCommand : IRequest<ServiceResponse<NewsletterSubscriptionDto>>
    {
        //public Guid Id { get; set; }
        public string Email { get; set; }
        public bool? isVerified { get; set; }
        public int? Counter { get; set; }
    }
}