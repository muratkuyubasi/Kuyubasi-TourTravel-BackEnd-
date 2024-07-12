using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class RegionListDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int RegionRecordId { get; set; }
        public Guid Code { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
    }
}
