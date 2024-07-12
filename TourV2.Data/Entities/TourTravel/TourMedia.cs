using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourMedia
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public bool IsCover { get; set; }
        public bool IsActive { get; set; }
    }
}
