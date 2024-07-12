using TourV2.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateUserClaimCommand : IRequest<ServiceResponse<UserClaimDto>>
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
