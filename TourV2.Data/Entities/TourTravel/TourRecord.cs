using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourRecord: BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int TourId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string LanguageCode { get; set; }
        
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
