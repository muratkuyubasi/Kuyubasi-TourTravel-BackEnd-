using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class CategoryRecord
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int CategoryId { get; set; }
        [MaxLength(350)]
        public string Slug { get; set; }
        [MaxLength(300)]
        public string Title { get; set; }
        [MaxLength(3)]
        public string LanguageCode { get; set; }
        public bool ShowCase { get; set; } = false;
        public bool IsActive { get; set; }
        public string Image { get; set; }
    }
}
