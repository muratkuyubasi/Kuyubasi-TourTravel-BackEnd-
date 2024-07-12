﻿using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateActionCommand : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
