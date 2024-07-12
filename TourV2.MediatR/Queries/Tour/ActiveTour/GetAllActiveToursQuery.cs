using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;

namespace TourV2.MediatR.Queries
{
    public class GetAllActiveToursQuery: IRequest<List<ActiveTourDto>>
    {
        public string LanguageCode { get; set; }
    }

    public class GetAllActiveToursSearchQuery : IRequest<List<ActiveTourDto>>
    {
        [Required]
        public string LanguageCode { get; set; }

        //Opsiyonel
        public string TourName { get; set; }
        public string PeriodName { get; set; }
        public string RegionName { get; set; }
        public string CategoryName { get; set; }
        public string DepartureName { get; set; }
        public string TransportationName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishDate { get; set; }

        //General Search
        public string SearchTerm { get; set; }
    }
}
