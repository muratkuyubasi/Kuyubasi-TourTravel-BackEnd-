using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class AppSettingProfile : Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSettingDto, AppSetting>().ReverseMap();
            CreateMap<AddAppSettingCommand, AppSetting>();
            CreateMap<UpdateAppSettingCommand, AppSetting>().ReverseMap();

        }
    }
}
