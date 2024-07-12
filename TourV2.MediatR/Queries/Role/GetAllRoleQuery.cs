﻿using TourV2.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace TourV2.MediatR.Queries
{
    public class GetAllRoleQuery : IRequest<List<RoleDto>>
    {
    }
}
