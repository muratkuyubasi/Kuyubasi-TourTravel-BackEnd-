using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class FrontAnnouncementDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
        public List<FrontAnnouncementRecordDto> FrontAnnouncementRecords { get; set; }
    }
    public class FrontAnnouncementRecordDto
    {
        public int Id { get; set; }
        public int FrontAnnouncementId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string FileUrl { get; set; }
        public string Link { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
        public FrontAnnouncement FrontAnnouncement { get; set; }
    }
}
