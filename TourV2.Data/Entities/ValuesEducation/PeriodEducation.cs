using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Entities
{
    public class PeriodEducation
    {
        public PeriodEducation()
        {
            Educations = new HashSet<EducationForm>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<EducationForm> Educations { get; set; }
    }
}
