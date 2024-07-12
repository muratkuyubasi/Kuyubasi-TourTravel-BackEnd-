using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class AddAppSettingCommandHandler : IRequestHandler<AddAppSettingCommand, ServiceResponse<AppSettingDto>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddAppSettingCommandHandler> _logger;
        public AddAppSettingCommandHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddAppSettingCommandHandler> logger
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<AppSettingDto>> Handle(AddAppSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("AppSetting already exist.");
                return ServiceResponse<AppSettingDto>.Return409("App Setting already exist.");
            }
            var entity = _mapper.Map<AppSetting>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _appSettingRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AppSettingDto>.Return500();
            }
            var entityDto = _mapper.Map<AppSettingDto>(entity);
            return ServiceResponse<AppSettingDto>.ReturnResultWith200(entityDto);
        }
    }
}
