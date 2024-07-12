using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ServiceResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserInfoToken _userInfoToken;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserCommandHandler> _logger;
        public AddUserCommandHandler(
            IMapper mapper,
            UserManager<User> userManager,
            UserInfoToken userInfoToken,
            ILogger<AddUserCommandHandler> logger
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByNameAsync(request.Email);
            if (appUser != null)
            {
                _logger.LogError("Email already exist for another user.");
                return ServiceResponse<UserDto>.Return409("Email already exist for another user.");
            }
            var entity = _mapper.Map<User>(request);
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedDate = DateTime.Now.ToLocalTime();
            entity.Id = Guid.NewGuid();
            IdentityResult result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                string code = await _userManager.GeneratePasswordResetTokenAsync(entity);
                IdentityResult passwordResult = await _userManager.ResetPasswordAsync(entity, code, request.Password);
                if (!passwordResult.Succeeded)
                {
                    return ServiceResponse<UserDto>.Return500();
                }
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(entity));
        }
    }
}
