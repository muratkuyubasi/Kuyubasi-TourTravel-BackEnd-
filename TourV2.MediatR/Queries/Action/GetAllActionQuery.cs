using TourV2.Data.Dto;
using MediatR;
using System.Collections.Generic;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetAllActionQuery : IRequest<ServiceResponse<List<ActionDto>>>
    {
    }
}
