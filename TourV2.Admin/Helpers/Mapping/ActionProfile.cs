using AutoMapper;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using Action = TourV2.Data.Action;

namespace TourV2.Admin.Helpers.Mapping
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
