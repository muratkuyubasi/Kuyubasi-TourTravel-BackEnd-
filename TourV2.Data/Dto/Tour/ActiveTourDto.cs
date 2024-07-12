using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class ActiveTourDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int TourRecordId { get; set; }
        public  TourRecord TourRecord { get; set; }
        public int PeriodRecordId { get; set; }
        public  PeriodRecord PeriodRecord { get; set; }
        public int RegionRecordId { get; set; }
        public  RegionRecord RegionRecord { get; set; }
        public int ReservationTotalPerson { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsChild { get; set; }
        public int ChildQuota { get; set; }
        public int Quota { get; set; }
        public bool ShowCase { get; set; }
        public int DayCount { get; set; }
        public string? YoutubeLink { get; set; }

        public int ClickCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsActive { get; set; }
        public  List<TourCategory> TourCategories { get; set; }
        public  List<TourDeparture> TourDepartures { get; set; }
        public  List<TourMedia> TourMedias { get; set; }
        public  List<TourPrice> TourPrices { get; set; }
        public  List<TourDay> TourDays { get; set; }
        public  List<TourSpecification> TourSpecifications { get; set; }
        public  TourTransportation TourTransportation { get; set; }
        public List<TourComment> TourComments { get; set; }
        public List<TourClick> TourClicks { get; set; }
        
    }
}
