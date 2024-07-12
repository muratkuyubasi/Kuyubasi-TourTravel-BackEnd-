using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleClaim, RoleClaimDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<AddRoleCommand, Role>();
            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
