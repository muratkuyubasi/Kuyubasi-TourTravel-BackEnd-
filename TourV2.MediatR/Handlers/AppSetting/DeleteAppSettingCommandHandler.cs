using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
    public class DeleteAppSettingCommandHandler : IRequestHandler<DeleteAppSettingCommand, ServiceResponse<AppSettingDto>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<DeleteAppSettingCommandHandler> _logger;
        public DeleteAppSettingCommandHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<DeleteAppSettingCommandHandler> logger
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<AppSettingDto>> Handle(DeleteAppSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("AppSetting Not Found.");
                return ServiceResponse<AppSettingDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            _appSettingRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AppSettingDto>.Return500();
            }
            return ServiceResponse<AppSettingDto>.ReturnResultWith204();
        }
    }
}
