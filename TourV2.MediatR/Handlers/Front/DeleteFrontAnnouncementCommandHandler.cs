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
    public class DeleteFrontAnnouncementCommandHandler : IRequestHandler<DeleteFrontAnnouncementCommand, ServiceResponse<FrontAnnouncementDto>>
    {
        private readonly UserInfoToken _userInfoToken;
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly ILogger<DeleteFrontAnnouncementCommandHandler> _logger;
        public DeleteFrontAnnouncementCommandHandler(
            UserInfoToken userInfoToken,
            IFrontAnnouncementRepository FrontAnnouncementRepository,
            IUnitOfWork<TourContext> uow,
            ILogger<DeleteFrontAnnouncementCommandHandler> logger
            )
        {
            _userInfoToken = userInfoToken;
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(DeleteFrontAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _FrontAnnouncementRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<FrontAnnouncementDto>.Return404();
            }
            _FrontAnnouncementRepository.Delete(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementDto>.Return500();
            }
            return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith204();
        }
    }
}
