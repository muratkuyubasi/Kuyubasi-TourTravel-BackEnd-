using TourV2.Data.Dto;
using MediatR;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
