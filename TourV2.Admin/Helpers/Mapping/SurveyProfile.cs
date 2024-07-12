using AutoMapper;
using TourV2.Data.Dto;
using TourV2.Data.Dto.Tour;
using TourV2.Data.Entities.TourTravel;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Commands.Survery;

namespace TourV2.Admin.Helpers.Mapping
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile()
        {
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<AddSurveryCommand, Survey>();
        }
    }
}
