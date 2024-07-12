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
    public class ConfirmTourCommentCommandHandler : IRequestHandler<ConfirmTourCommentCommand, ServiceResponse<TourCommentDto>>
    {
        private readonly ITourCommentRepository _TourCommentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ConfirmTourCommentCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public ConfirmTourCommentCommandHandler(ITourCommentRepository TourCommentRepository, IMapper mapper, ILogger<ConfirmTourCommentCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _TourCommentRepository = TourCommentRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourCommentDto>> Handle(ConfirmTourCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _TourCommentRepository.FindAsync(request.Id);
            entity.IsActive = !entity.IsActive;

            _TourCommentRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<TourCommentDto>.Return500();
            }

            var entityDto = _mapper.Map<TourCommentDto>(entity);
            return ServiceResponse<TourCommentDto>.ReturnResultWith200(entityDto);


        }
    }
}