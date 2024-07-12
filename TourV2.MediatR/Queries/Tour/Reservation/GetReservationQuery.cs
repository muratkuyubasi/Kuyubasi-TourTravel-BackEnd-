using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetReservationQuery : IRequest<ServiceResponse<TourReservationDto>>
    {
        public Guid Id { get; set; }
    }
}
