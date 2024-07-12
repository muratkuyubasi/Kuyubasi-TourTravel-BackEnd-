using TourV2.Data.Dto;
using MediatR;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
