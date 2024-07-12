using AutoMapper;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Data.Dto;
using TourV2.Data.Entities;
using TourV2.MediatR.Commands.EducaitonForm;
using TourV2.MediatR.Commands.MosqueRepository;
using TourV2.MediatR.Commands.State;
using TourV2.MediatR.Commands.PeriodEducation;

namespace TourV2.Admin.Helpers.Mapping
{
    public class EducationFormProfile : Profile
    {
        public EducationFormProfile()
        {
            CreateMap<EducationForm, EducationFormDTO>().ReverseMap();
            CreateMap<AddEducationFormCommand, EducationForm>();
            //CreateMap<UpdateActionCommand, EducationForm>();


            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<AddStateCommand, State>();

            CreateMap<Mosque, MosqueDto>().ReverseMap();
            CreateMap<AddMosqueCommand, Mosque>();

            CreateMap<PeriodEducation, PeriodEducationDto>().ReverseMap();
            CreateMap<AddPeriodEducationCommand, PeriodEducation>();
        }
    }
}
