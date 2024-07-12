using TourV2.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace TourV2.MediatR.Queries
{
    public class GetRoleUsersQuery : IRequest<List<UserRoleDto>>
    {
        public Guid RoleId { get; set; }
    }
}
