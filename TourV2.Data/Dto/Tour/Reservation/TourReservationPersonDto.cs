using System;
using System.ComponentModel.DataAnnotations.Schema;
using TourV2.Helper;

namespace TourV2.Data.Dto
{
    public class TourReservationPersonDto
    {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public int TourDepartureId { get; set; }
        public TourDeparture TourDeparture { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public string FilePath { get; set; }
        public string Uyruk { get; set; }
        public string StudentPath { get; set; }
    }
}
