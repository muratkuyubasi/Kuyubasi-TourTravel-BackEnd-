using TourV2.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateUserRoleCommand : IRequest<ServiceResponse<UserRoleDto>>
    {
        public Guid Id { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
