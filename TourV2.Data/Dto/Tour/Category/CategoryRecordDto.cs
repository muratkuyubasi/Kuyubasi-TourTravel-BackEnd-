using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class CategoryRecordDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public bool ShowCase { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

    }
}
