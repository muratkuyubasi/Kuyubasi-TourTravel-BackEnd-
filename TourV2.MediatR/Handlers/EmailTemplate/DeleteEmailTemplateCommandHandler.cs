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
    public class DeleteEmailTemplateCommandHandler : IRequestHandler<DeleteEmailTemplateCommand, ServiceResponse<bool>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateAppSettingCommandHandler> _logger;
        public DeleteEmailTemplateCommandHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<UpdateAppSettingCommandHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email Template Not Found.");
                return ServiceResponse<bool>.Return404();
            }
            entityExist.IsDeleted = true;
            _emailTemplateRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith204();
        }
    }
}
