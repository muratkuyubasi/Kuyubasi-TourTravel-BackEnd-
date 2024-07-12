using AutoMapper;
using TourV2.Data.Dto;
using TourV2.Data;
using TourV2.MediatR.Commands;

namespace TourV2.Admin.Helpers.Mapping
{
    public class ActiveTourProfile: Profile
    {
        public ActiveTourProfile()
        {
            CreateMap<ActiveTour, ActiveTourDto>().ReverseMap();
            CreateMap<AddActiveTourCommand, ActiveTour>();
            CreateMap<TourReservation, TourReservationDto>().ReverseMap();
            CreateMap<TourReservationPerson, TourReservationPersonDto>().ReverseMap();
            CreateMap<AddTourReservationCommand, TourReservation>();
            CreateMap<AddTourReservationPersonCommand, TourReservationPerson>();
        }
    }
}
