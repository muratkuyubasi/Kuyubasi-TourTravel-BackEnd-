using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetTourCommentByTourMainIdQuery : IRequest<ServiceResponse<List<TourCommentDto>>>
    {
        public int ActiveTourId { get; set; }
    }
}
