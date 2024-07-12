using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateDepartureCommand : IRequest<ServiceResponse<DepartureRecordDto>>
    {
        public Guid Code { get; set; }
        public int DepartureId { get; set; }
        public bool IsMain { get; set; }
        public string LatLng { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
