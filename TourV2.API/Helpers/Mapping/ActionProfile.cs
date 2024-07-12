using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;

namespace TourV2.API.Helpers.Mapping
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<AddActionCommand, Action>();
            CreateMap<UpdateActionCommand, Action>();
        }
    }
}
