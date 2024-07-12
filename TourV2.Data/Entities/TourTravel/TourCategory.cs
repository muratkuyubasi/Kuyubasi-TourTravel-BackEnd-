using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourCategory
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        [ForeignKey("CategoryRecordId")]
        public int CategoryRecordId { get; set; }
        public virtual CategoryRecord CategoryRecord { get; set; }
    }
}
