﻿using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeleteRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
