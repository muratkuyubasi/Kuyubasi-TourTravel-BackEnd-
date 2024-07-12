using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourTransportation
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string Title { get; set; }
    }
}
