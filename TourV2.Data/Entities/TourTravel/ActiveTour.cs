using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Entities;

namespace TourV2.Data
{
    public class ActiveTour: BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("TourRecordId")]
        public int TourRecordId { get; set; }
        public virtual TourRecord TourRecord { get; set; }
        [ForeignKey("PeriodRecordId")]
        public int PeriodRecordId { get; set; }
        public virtual PeriodRecord PeriodRecord { get; set; }
        [ForeignKey("RegionRecordId")]
        public int RegionRecordId { get; set; }
        public virtual RegionRecord RegionRecord { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsChild { get; set; }
        public int ChildQuota { get; set; }
        public int Quota { get; set; }
        public int DayCount { get; set; }
        public string? YoutubeLink { get; set; }
        public bool ShowCase { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public virtual ICollection<TourCategory> TourCategories { get; set; }
        public virtual ICollection<TourDeparture> TourDepartures { get; set; }
        public virtual ICollection<TourMedia> TourMedias { get; set; }
        public virtual ICollection<TourPrice> TourPrices { get; set; }
        public virtual ICollection<TourDay> TourDays { get; set; }
        public virtual ICollection<TourSpecification> TourSpecifications { get; set; }
        public virtual TourTransportation TourTransportation { get; set; }
        public virtual ICollection<TourReservation> TourReservations { get; set; }
        public virtual ICollection<TourComment> TourComments { get; set; }
        public virtual ICollection<TourClick> TourClicks { get; set; }
        public virtual ICollection<CostCalculation> CostCalculations { get; set; }

        public Guid Code { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
