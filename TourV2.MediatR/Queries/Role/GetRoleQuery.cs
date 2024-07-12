using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetRoleQuery: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
