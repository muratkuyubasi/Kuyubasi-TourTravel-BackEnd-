using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetTourReservationQuery : IRequest<ServiceResponse<List<TourReservationDto>>>
    {
        public int Id { get; set; }
    }
}
