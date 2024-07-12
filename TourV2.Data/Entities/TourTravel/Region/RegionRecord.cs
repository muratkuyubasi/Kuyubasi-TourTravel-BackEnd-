using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class RegionRecord
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int RegionId { get; set; }
        [MaxLength(1000)]
        public string Slug { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }
        [MaxLength(3)]
        public string LanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
