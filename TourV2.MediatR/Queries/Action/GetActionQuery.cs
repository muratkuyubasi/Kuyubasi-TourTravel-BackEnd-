using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetActionQuery : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
