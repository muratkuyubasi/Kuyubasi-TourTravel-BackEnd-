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
    public class DeleteContactMessageCommandHandler : IRequestHandler<DeleteContactMessageCommand, ServiceResponse<ContactMessageDto>>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteContactMessageCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteContactMessageCommandHandler(IContactMessageRepository ContactMessageRepository, IMapper mapper, ILogger<DeleteContactMessageCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _contactMessageRepository = ContactMessageRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<ContactMessageDto>> Handle(DeleteContactMessageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _contactMessageRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<ContactMessageDto>.Return409("Kayıt Bulunamadı");
            }

            _contactMessageRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<ContactMessageDto>.Return500();
            }

            var entityDto = _mapper.Map<ContactMessageDto>(entityExist);
            return ServiceResponse<ContactMessageDto>.ReturnResultWith200(entityDto);


        }
    }
}