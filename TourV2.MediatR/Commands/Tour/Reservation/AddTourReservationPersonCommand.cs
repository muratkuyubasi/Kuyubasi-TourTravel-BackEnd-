using MediatR;
using System;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddTourReservationPersonCommand : IRequest<ServiceResponse<TourReservationPersonDto>>
    {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public int TourDepartureId { get; set; }
        public int TourPriceId { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public string FilePath { get; set; }
    }
}
