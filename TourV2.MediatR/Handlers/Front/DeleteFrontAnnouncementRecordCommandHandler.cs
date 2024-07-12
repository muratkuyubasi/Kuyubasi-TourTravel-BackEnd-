using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Repository;
using System;
using TourV2.Domain;
using TourV2.Common.UnitOfWork;
using TourV2.Helper;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TourV2.MediatR.Handlers
{
    public class DeleteFrontAnnouncementRecordCommandHandler : IRequestHandler<DeleteFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly UserInfoToken _userInfoToken;
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly ILogger<DeleteFrontAnnouncementRecordCommandHandler> _logger;
        public DeleteFrontAnnouncementRecordCommandHandler(
            UserInfoToken userInfoToken,
            IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IUnitOfWork<TourContext> uow,
            ILogger<DeleteFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _userInfoToken = userInfoToken;
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(DeleteFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _FrontAnnouncementRecordRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<FrontAnnouncementRecordDto>.Return404();
            }
            _FrontAnnouncementRecordRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith204();
        }
    }
}
