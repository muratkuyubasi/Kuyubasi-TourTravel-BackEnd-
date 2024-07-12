using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<UserAllowedIPDto, UserAllowedIP>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<SocialLoginCommand, User>();
            CreateMap<ResetPasswordCommand, UserDto>();
        }
    }
}
