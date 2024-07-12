using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetAllContactMessageQuery : IRequest<ServiceResponse<List<ContactMessageDto>>>
    {
    }
    public class GetAllTourRequestsQuery : IRequest<ServiceResponse<List<ContactMessageDto>>>
    {
    }
}