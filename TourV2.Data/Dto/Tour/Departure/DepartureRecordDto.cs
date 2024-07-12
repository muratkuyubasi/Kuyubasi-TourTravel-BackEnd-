using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class DepartureRecordDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int DepartureId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public string LatLng { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
