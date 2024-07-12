using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetAllNewsletterSubscriptionQuery : IRequest<ServiceResponse<List<NewsletterSubscriptionDto>>>
    {
    }
}