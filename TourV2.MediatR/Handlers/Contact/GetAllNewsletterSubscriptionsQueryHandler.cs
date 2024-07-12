using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllNewsletterSubscriptionsQueryHandler: IRequestHandler<GetAllNewsletterSubscriptionQuery, ServiceResponse<List<NewsletterSubscriptionDto>>>
    {
        public readonly INewsletterSubscriptionRepository _newsletterSubscriptionRepository;
        public readonly IMapper _mapper;

        public GetAllNewsletterSubscriptionsQueryHandler(INewsletterSubscriptionRepository NewsletterSubscriptionRepository, IMapper mapper)
        {
            _newsletterSubscriptionRepository = NewsletterSubscriptionRepository;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<NewsletterSubscriptionDto>>> Handle(GetAllNewsletterSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var newsletterSubscriptions = await _newsletterSubscriptionRepository.All.ToListAsync();
            return ServiceResponse<List<NewsletterSubscriptionDto>>.ReturnResultWith200(_mapper.Map<List<NewsletterSubscriptionDto>>(newsletterSubscriptions));
        }
    }
}
