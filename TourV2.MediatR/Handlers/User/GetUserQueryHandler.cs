using AutoMapper;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ServiceResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserQueryHandler> _logger;
        public GetUserQueryHandler(
           IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUserQueryHandler> logger
            )
        {

            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.AllIncluding(c => c.UserRoles, cs => cs.UserClaims, ip => ip.UserAllowedIPs).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (entity != null)
                return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(entity));
            else
            {
                _logger.LogError("User not found");
                return ServiceResponse<UserDto>.ReturnFailed(404, "User not found");
            }
        }
    }
}
