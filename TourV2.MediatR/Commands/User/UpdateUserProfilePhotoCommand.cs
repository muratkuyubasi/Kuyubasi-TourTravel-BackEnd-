using MediatR;
using Microsoft.AspNetCore.Http;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateUserProfilePhotoCommand : IRequest<ServiceResponse<UserDto>>
    {
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }
    }
}
