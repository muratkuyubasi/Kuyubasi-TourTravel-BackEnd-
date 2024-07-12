using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class SocialLoginCommandHandler : IRequestHandler<SocialLoginCommand, ServiceResponse<UserAuthDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILoginAuditRepository _loginAuditRepository;
        private readonly IHubContext<UserHub, IHubClient> _hubContext;
        private readonly IRoleRepository _roleRepository;
        public SocialLoginCommandHandler(
            IMapper mapper,
            UserManager<User> userManager,
            IUserRepository userRepository,
            ILoginAuditRepository loginAuditRepository,
             IHubContext<UserHub, IHubClient> hubContext,
             IRoleRepository roleRepository
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _loginAuditRepository = loginAuditRepository;
            _hubContext = hubContext;
            _roleRepository = roleRepository;
        }
        public async Task<ServiceResponse<UserAuthDto>> Handle(SocialLoginCommand request, CancellationToken cancellationToken)
        {
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Success.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Provider = request.Provider
            };

            var appUser = await _userManager.FindByNameAsync(request.Email);
            if (appUser != null)
            {
                appUser.FirstName = request.FirstName;
                appUser.LastName = request.LastName;
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                var entity = _mapper.Map<User>(request);
                entity.Id = Guid.NewGuid();
                entity.IsActive = true;

                // Assign Social medial Role to user
                var socialMediaRole = _roleRepository.All.Where(c => c.Name.ToLower() == "social media").FirstOrDefault();
                if (socialMediaRole != null)
                {
                    entity.UserRoles.Add(new UserRole
                    {
                        RoleId = socialMediaRole.Id,
                        UserId = entity.Id
                    });
                }

                IdentityResult result = await _userManager.CreateAsync(entity);
                if (!result.Succeeded)
                {
                    loginAudit.Status = LoginStatus.Error.ToString();
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.Return500();
                }
                appUser = await _userManager.FindByNameAsync(request.Email);
            }

            await _loginAuditRepository.LoginAudit(loginAudit);
            var authUser = await _userRepository.BuildUserAuthObject(appUser);
            var onlineUser = new UserInfoToken
            {
                Email = authUser.Email,
                Id = authUser.Id.ToString()
            };
            await _hubContext.Clients.All.Joined(onlineUser);
            return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
        }
    }
}
