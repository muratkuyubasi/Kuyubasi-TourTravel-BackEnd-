using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class DepartureDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public string LatLng { get; set; }
        public DateTime DepartureTime { get; set; }
        public List<DepartureRecord> DepartureRecords { get; set; }
    }
}
