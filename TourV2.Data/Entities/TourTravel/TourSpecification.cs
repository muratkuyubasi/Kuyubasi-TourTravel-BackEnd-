using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourSpecification
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string Specification { get; set; }
        public bool InPrice { get; set; }
    }
}
