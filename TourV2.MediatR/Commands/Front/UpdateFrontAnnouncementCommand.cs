using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateFrontAnnouncementCommand: IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public int Id { get; set; }
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
    }
}
