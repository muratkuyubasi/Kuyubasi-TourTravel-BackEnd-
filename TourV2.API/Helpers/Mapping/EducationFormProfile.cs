using AutoMapper;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Data.Entities;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Commands.EducaitonForm;
using TourV2.MediatR.Commands.MosqueRepository;
using TourV2.MediatR.Commands.State;

namespace TourV2.API.Helpers.Mapping
{
    public class EducationFormProfile : Profile
    {
        public EducationFormProfile()
        {
            CreateMap<EducationForm, EducationFormDTO>().ReverseMap();
            CreateMap<AddEducationFormCommand, EducationForm>();
            //CreateMap<UpdateActionCommand, EducationForm>();


            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<AddStateCommand, StateDto>();

            CreateMap<Mosque, MosqueDto>().ReverseMap();
            CreateMap<AddMosqueCommand, MosqueDto>();
        }
    }
}
