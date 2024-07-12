using AutoMapper;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class AddFrontAnnouncementCommandHandler : IRequestHandler<AddFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddFrontAnnouncementCommandHandler> _logger;
        public AddFrontAnnouncementCommandHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddFrontAnnouncementCommandHandler> logger
            )
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(AddFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FrontAnnouncement>(request);
            entity.Code = Guid.NewGuid();

            _FrontAnnouncementRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementDto>(entity);
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(entityDto); 
        }
    }
}
