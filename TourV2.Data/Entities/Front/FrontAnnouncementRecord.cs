using System.ComponentModel.DataAnnotations;

namespace TourV2.Data
{
    public class FrontAnnouncementRecord
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
        public virtual FrontAnnouncement FrontAnnouncement { get; set; }
    }
}
