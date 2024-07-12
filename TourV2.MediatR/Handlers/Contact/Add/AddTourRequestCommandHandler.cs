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
    public class AddTourRequestCommandHandler : IRequestHandler<AddTourRequestCommand, ServiceResponse<ContactMessageDto>>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddContactMessageCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddTourRequestCommandHandler(IContactMessageRepository ContactMessageRepository, IMapper mapper, ILogger<AddContactMessageCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _contactMessageRepository = ContactMessageRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<ContactMessageDto>> Handle(AddTourRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ContactMessage>(request);
            entity.CreationDate = DateTime.Now;
            entity.Subject = "TTPTalep";
            entity.Type = 2;

            _contactMessageRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<ContactMessageDto>.Return500();
            }

            var entityDto = _mapper.Map<ContactMessageDto>(entity);
            return ServiceResponse<ContactMessageDto>.ReturnResultWith200(entityDto);


        }
    }
}