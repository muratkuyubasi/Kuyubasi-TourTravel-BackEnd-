using TourV2.Data.Dto;
using MediatR;
using System.Collections.Generic;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
