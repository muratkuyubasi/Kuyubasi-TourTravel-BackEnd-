using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Resources
{
    public class CourseResource : ResourceParameter
    {
        public CourseResource() : base("Title")
        {
        }

        public string Code { get; set; }
        public bool IsPopuler { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublish { get; set; }
        public string IsActive { get; set; }
        public bool AllCourse { get; set; }

    }
}
