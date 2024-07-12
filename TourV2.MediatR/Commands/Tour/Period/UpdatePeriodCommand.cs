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
    public class UpdatePeriodCommand : IRequest<ServiceResponse<PeriodRecordDto>>
    {
        public Guid Code { get; set; }
        public int PeriodId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string LanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
