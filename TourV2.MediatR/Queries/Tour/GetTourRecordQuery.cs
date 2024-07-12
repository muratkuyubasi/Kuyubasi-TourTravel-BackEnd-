using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetTourRecordQuery : IRequest<ServiceResponse<TourRecordDto>>
    {
        public Guid Id { get; set; }
    }
}
