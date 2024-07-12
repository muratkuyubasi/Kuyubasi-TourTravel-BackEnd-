using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeleteFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public Guid Code { get; set; }
    }
}
