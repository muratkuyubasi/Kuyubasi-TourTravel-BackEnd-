using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<AddPageCommand, Page>();
            CreateMap<UpdatePageCommand, Page>();
        }
    }
}
