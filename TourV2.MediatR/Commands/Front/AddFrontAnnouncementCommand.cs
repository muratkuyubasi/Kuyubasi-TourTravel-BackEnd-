using TourV2.Data.Dto;
using MediatR;
using System.Collections.Generic;
using TourV2.Helper;
using System;

namespace TourV2.MediatR.Commands
{
    public class AddFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
        //public List<FrontAnnouncementRecordDto> FrontAnnouncementRecords { get; set; }
    }
}
