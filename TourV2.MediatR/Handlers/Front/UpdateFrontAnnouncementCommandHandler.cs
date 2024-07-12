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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class UpdateFrontAnnouncementCommandHandler : IRequestHandler<UpdateFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateFrontAnnouncementCommandHandler> _logger;
        public UpdateFrontAnnouncementCommandHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<UpdateFrontAnnouncementCommandHandler> logger
            )
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(UpdateFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _FrontAnnouncementRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (entityExist != null)
            {
                _logger.LogError("FrontAnnouncement Already Exist.");
                return ServiceResponse<FrontAnnouncementDto>.Return409("FrontAnnouncement Already Exist.");
            }

            entityExist.Order = request.Order;
            entityExist.IsSlider = request.IsSlider;
            entityExist.IsNews = request.IsNews;
            entityExist.IsAnnouncement = request.IsAnnouncement;

            _FrontAnnouncementRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementDto>(entityExist));
        }
    }
}
