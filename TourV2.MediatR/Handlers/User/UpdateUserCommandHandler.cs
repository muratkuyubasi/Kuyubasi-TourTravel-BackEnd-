using AutoMapper;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRoleRepository _userRoleRepository;
        IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUserAllowedIPRepository _userAllowedIPRepository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        public UpdateUserCommandHandler(
            IUserRoleRepository userRoleRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserManager<User> userManager,
            UserInfoToken userInfoToken,
            IUserAllowedIPRepository userAllowedIPRepository,
            ILogger<UpdateUserCommandHandler> logger
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _userAllowedIPRepository = userAllowedIPRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            appUser.IsActive = request.IsActive;
            appUser.ModifiedDate = DateTime.Now.ToLocalTime();
            appUser.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            IdentityResult result = await _userManager.UpdateAsync(appUser);

            // Update User's Role
            var userRoles = _userRoleRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var rolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.AddRange(_mapper.Map<List<UserRole>>(rolesToAdd));
            var rolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.RemoveRange(rolesToDelete);

            //Update User's Allowed IPs
            var userAllowedIPs = _userAllowedIPRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var ipsToAdd = request.UserAllowedIPs
                .Where(c => !userAllowedIPs.Select(c => c.IPAddress).Contains(c.IPAddress))
                .Select(cs => new UserAllowedIP
                {
                    IPAddress = cs.IPAddress,
                    UserId = appUser.Id
                })
                .ToList();
            _userAllowedIPRepository.AddRange(ipsToAdd);
            var ipsToDelete = userAllowedIPs
                .Where(c => !request.UserAllowedIPs.Select(cs => cs.IPAddress).Contains(c.IPAddress))
                .ToList();
            _userAllowedIPRepository.RemoveRange(ipsToDelete);

            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(appUser));
        }
    }
}
