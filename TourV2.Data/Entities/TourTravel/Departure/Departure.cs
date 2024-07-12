using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class Departure
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
     
        public virtual ICollection<DepartureRecord> DepartureRecords { get; set; }
    }
}
