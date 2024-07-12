using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetAllTourCommentQuery : IRequest<ServiceResponse<List<TourCommentDto>>>
    {
    }
    public class GetAllTourPopularCommentQuery : IRequest<ServiceResponse<List<TourCommentDto>>>
    {
    }
}