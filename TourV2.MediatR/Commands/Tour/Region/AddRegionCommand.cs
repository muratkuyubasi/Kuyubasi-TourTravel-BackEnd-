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
    public class AddRegionCommand : IRequest<ServiceResponse<RegionDto>>
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }

        public List<RegionRecordDto> RegionRecords { get; set; }
    }
}
