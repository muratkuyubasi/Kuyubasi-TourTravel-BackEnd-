using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetTourReservationPersonQuery : IRequest<ServiceResponse<TourReservationPersonDto>>
    {
        public int Id { get; set; }
    }
}
