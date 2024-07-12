using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace TourV2.MediatR.Handlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, ServiceResponse<UserAuthDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILoginAuditRepository _loginAuditRepository;
        private readonly IHubContext<UserHub, IHubClient> _hubContext;

        public UserLoginCommandHandler(
            IUserRepository userRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILoginAuditRepository loginAuditRepository,
            IHubContext<UserHub, IHubClient> hubContext
            )
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _loginAuditRepository = loginAuditRepository;
            _hubContext = hubContext;
        }
        public async Task<ServiceResponse<UserAuthDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Error.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result.Succeeded)
            {
                var userInfo = await _userRepository
                    .AllIncluding(c => c.UserAllowedIPs)
                    .Where(c => c.UserName == request.UserName)
                    .FirstOrDefaultAsync();
                if (!userInfo.IsActive)
                {
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.ReturnFailed(401, "UserName Or Password is InCorrect.");
                }

                if (userInfo.UserAllowedIPs.Any() && !userInfo.UserAllowedIPs.Any(c => c.IPAddress == request.RemoteIp))
                {
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.ReturnFailed(401, "You don't have access on this IP Address.");
                }
                loginAudit.Status = LoginStatus.Success.ToString();
                await _loginAuditRepository.LoginAudit(loginAudit);
                var authUser = await _userRepository.BuildUserAuthObject(userInfo);
                var onlineUser = new UserInfoToken
                {
                    Email = authUser.Email,
                    Id = authUser.Id.ToString()
                };
                await _hubContext.Clients.All.Joined(onlineUser);
                return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
            }
            else
            {
                await _loginAuditRepository.LoginAudit(loginAudit);
                return ServiceResponse<UserAuthDto>.ReturnFailed(401, "UserName Or Password is InCorrect.");
            }
        }
    }
}
