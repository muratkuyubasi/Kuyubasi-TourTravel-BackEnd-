using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.API.Helpers.Mapping
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailSMTPSetting, EmailSMTPSettingDto>().ReverseMap();
            CreateMap<EmailSMTPSetting, AddEmailSMTPSettingCommand>().ReverseMap();
            CreateMap<EmailSMTPSetting, UpdateEmailSMTPSettingCommand>().ReverseMap();
        }
    }
}
