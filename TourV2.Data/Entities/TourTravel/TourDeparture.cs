using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourDeparture
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool IsMain { get; set; }
        [ForeignKey("DepartureRecordId")]
        public int DepartureRecordId { get; set; }
        public virtual DepartureRecord DepartureRecord { get; set; }

    }
}
