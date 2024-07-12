using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class PeriodDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public List<PeriodRecord> PeriodRecords { get; set; }
    }
}
