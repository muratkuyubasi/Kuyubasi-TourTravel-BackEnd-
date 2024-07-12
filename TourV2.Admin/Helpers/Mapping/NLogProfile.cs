using AutoMapper;
using TourV2.Data.Dto;

namespace TourV2.Admin.Helpers.Mapping
{
    public class NLogProfile : Profile
    {
        public NLogProfile()
        {
            CreateMap<Data.NLog, NLogDto>().ReverseMap();
        }
    }
}
