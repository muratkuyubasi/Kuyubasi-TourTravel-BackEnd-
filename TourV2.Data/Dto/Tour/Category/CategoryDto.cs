using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public List<CategoryRecord> CategoryRecords { get; set; }
    }
}
