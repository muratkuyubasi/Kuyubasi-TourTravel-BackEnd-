using MediatR;
using System;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddTourClickCommand : IRequest<ServiceResponse<TourClickDto>>
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        public int ActiveTourId { get; set; }
        public string IpAddress { get; set; }
        //public virtual ActiveTour ActiveTour { get; set; }
    }
}