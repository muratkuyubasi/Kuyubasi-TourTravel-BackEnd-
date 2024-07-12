using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetLogQuery : IRequest<ServiceResponse<NLogDto>>
    {
        public Guid Id { get; set; }
    }
}
