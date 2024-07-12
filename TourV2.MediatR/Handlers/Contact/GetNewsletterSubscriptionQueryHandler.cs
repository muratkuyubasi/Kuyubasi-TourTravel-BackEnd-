using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetNewsletterSubscriptionQueryHandler: IRequestHandler<GetNewsletterSubscriptionQuery, ServiceResponse<NewsletterSubscriptionDto>>
    {
        private readonly INewsletterSubscriptionRepository _newsletterSubscriptionRepository;
        private readonly IMapper _mapper;

        public GetNewsletterSubscriptionQueryHandler(INewsletterSubscriptionRepository NewsletterSubscriptionRepository,
            IMapper mapper)
        {
            _newsletterSubscriptionRepository = NewsletterSubscriptionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<NewsletterSubscriptionDto>> Handle(GetNewsletterSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var newsletterSubscription = _newsletterSubscriptionRepository.FindAsync(request.Id);
            if(newsletterSubscription != null)
            {
                return ServiceResponse<NewsletterSubscriptionDto>.ReturnResultWith200(_mapper.Map<NewsletterSubscriptionDto>(newsletterSubscription));
            }
            else
            {
                return ServiceResponse<NewsletterSubscriptionDto>.Return404();
            }
        }
    }
}
