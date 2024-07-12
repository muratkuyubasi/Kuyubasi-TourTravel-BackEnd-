using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateActiveTourCommand : IRequest<ServiceResponse<ActiveTourDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int TourRecordId { get; set; }
        public int PeriodRecordId { get; set; }
        public int RegionRecordId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsChild { get; set; }
        public int ChildQuota { get; set; }
        public int Quota { get; set; }
        public int DayCount { get; set; }
        public bool ShowCase { get; set; }
        public string? YoutubeLink { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsActive { get; set; }
        public List<TourCategory> TourCategories { get; set; }
        public TourTransportation TourTransportation { get; set; }
    }
}
