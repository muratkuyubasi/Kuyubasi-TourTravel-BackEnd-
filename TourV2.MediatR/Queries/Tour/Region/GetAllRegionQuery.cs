using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;

namespace TourV2.MediatR.Queries
{
    public class GetAllRegionsQuery: IRequest<List<RegionListDto>>
    {
        public string LanguageCode { get; set; }
    }
}
