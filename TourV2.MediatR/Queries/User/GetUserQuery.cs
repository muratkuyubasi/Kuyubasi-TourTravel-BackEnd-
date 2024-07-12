using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetUserQuery : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
