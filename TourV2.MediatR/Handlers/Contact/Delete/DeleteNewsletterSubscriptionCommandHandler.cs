using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class DeleteNewsletterSubscriptionCommandHandler : IRequestHandler<DeleteNewsletterSubscriptionCommand, ServiceResponse<NewsletterSubscriptionDto>>
    {
        private readonly INewsletterSubscriptionRepository _newsletterSubscriptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteNewsletterSubscriptionCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteNewsletterSubscriptionCommandHandler(INewsletterSubscriptionRepository NewsletterSubscriptionRepository, IMapper mapper, ILogger<DeleteNewsletterSubscriptionCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _newsletterSubscriptionRepository = NewsletterSubscriptionRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<NewsletterSubscriptionDto>> Handle(DeleteNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _newsletterSubscriptionRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<NewsletterSubscriptionDto>.Return409("Kayıt Bulunamadı");
            }

            _newsletterSubscriptionRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<NewsletterSubscriptionDto>.Return500();
            }

            var entityDto = _mapper.Map<NewsletterSubscriptionDto>(entityExist);
            return ServiceResponse<NewsletterSubscriptionDto>.ReturnResultWith200(entityDto);


        }
    }
}