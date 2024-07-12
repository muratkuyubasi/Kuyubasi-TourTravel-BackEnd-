using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class FrontAnnouncementProfile : Profile
    {
        public FrontAnnouncementProfile()
        {
            CreateMap<FrontAnnouncement, FrontAnnouncementDto>().ReverseMap();
            CreateMap<AddFrontAnnouncementCommand, FrontAnnouncement>().ReverseMap();
            CreateMap<UpdateFrontAnnouncementCommand, FrontAnnouncement>().ReverseMap();

            CreateMap<FrontAnnouncementRecord, FrontAnnouncementRecordDto>().ReverseMap();
            CreateMap<AddFrontAnnouncementRecordCommand, FrontAnnouncementRecord>().ReverseMap();
            CreateMap<UpdateFrontAnnouncementRecordCommand, FrontAnnouncementRecord>().ReverseMap();
        }
    }
}
