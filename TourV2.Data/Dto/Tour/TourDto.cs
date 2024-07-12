using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class TourDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public List<TourRecordDto> TourRecords { get; set; }
    }
}
