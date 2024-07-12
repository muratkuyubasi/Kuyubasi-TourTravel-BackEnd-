using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class AddNewsletterSubscriptionCommandHandler : IRequestHandler<AddNewsletterSubscriptionCommand, ServiceResponse<NewsletterSubscriptionDto>>
    {
        private readonly INewsletterSubscriptionRepository _newsletterSubscriptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddNewsletterSubscriptionCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddNewsletterSubscriptionCommandHandler(INewsletterSubscriptionRepository NewsletterSubscriptionRepository, IMapper mapper, ILogger<AddNewsletterSubscriptionCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _newsletterSubscriptionRepository = NewsletterSubscriptionRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<NewsletterSubscriptionDto>> Handle(AddNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<NewsletterSubscription>(request);
            entity.CreationDate = DateTime.Now;

            _newsletterSubscriptionRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<NewsletterSubscriptionDto>.Return500();
            }

            var entityDto = _mapper.Map<NewsletterSubscriptionDto>(entity);
            return ServiceResponse<NewsletterSubscriptionDto>.ReturnResultWith200(entityDto);


        }
    }
}