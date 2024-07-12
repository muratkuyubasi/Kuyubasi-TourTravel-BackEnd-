using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Helper;

namespace TourV2.Data
{
    public class TourReservationPerson
    {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public int TourDepartureId { get; set; }
        [ForeignKey("TourDepartureId")]
        public TourDeparture TourDeparture { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public string Uyruk { get; set; }
        public string StudentPath { get; set; }
        public string FilePath { get; set; }
        public int TourPriceId { get; set; }
        [ForeignKey("TourPriceId")]
        public TourPrice TourPrice { get; set; }
    }
}
