using System;
using System.Collections.Generic;

namespace TourV2.Data
{
    public class FrontAnnouncement : BaseEntity
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
        public virtual ICollection<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }
    }
}
