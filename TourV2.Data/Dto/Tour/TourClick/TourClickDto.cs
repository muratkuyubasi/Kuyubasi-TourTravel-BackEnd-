using System;

namespace TourV2.Data.Dto
{
    public class TourClickDto
    {
        public Guid Id { get; set; }
        public int ActiveTourId { get; set; }
        public string IpAddress { get; set; }
        //public virtual ActiveTour ActiveTour { get; set; }
    }
}
