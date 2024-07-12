using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetTourClickCountQuery : IRequest<ServiceResponse<int>>
    {
        public int? ActiveTourId { get; set; }
    }
}