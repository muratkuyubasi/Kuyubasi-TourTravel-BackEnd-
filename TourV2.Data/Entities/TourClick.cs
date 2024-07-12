using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourClick
    {
        public Guid Id { get; set; }
        public int ActiveTourId { get; set; }
        public string IpAddress { get; set; }
        //public virtual ActiveTour ActiveTour { get; set; }
    }
}
