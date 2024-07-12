using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class Category
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CategoryRecord> CategoryRecords { get; set; }
    }
}
