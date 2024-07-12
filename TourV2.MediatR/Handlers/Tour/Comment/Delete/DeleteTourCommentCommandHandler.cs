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
    public class DeleteTourCommentCommandHandler : IRequestHandler<DeleteTourCommentCommand, ServiceResponse<TourCommentDto>>
    {
        private readonly ITourCommentRepository _TourCommentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTourCommentCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteTourCommentCommandHandler(ITourCommentRepository TourCommentRepository, IMapper mapper, ILogger<DeleteTourCommentCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _TourCommentRepository = TourCommentRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourCommentDto>> Handle(DeleteTourCommentCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _TourCommentRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<TourCommentDto>.Return409("Kayıt Bulunamadı");
            }

            _TourCommentRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<TourCommentDto>.Return500();
            }

            var entityDto = _mapper.Map<TourCommentDto>(entityExist);
            return ServiceResponse<TourCommentDto>.ReturnResultWith200(entityDto);


        }
    }
}