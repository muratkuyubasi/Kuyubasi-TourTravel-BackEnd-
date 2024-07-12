using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Entities.TourTravel
{
    public class Survey
    {
        public int Id { get; set; }
        public int WhichTrip { get; set; }
        public int GeneralContactEvaluation { get; set; }
        public int InformationEvaluation { get; set; }
        public int TourTripGeneralEvaluation { get; set; }
        public int AeroplanePoint { get; set; }
        public int HotelPoint { get; set; }
        public int BusTransferPoint { get; set; }
        public int FoodPoint { get; set; }
        public int GuideLocalPoint { get; set; }
        public int GuidFromTurkeyPoint { get; set; }
        public int OfficersPoint { get; set; }
        public int OrganizationPoint { get; set; }
        public string ServicesOfferedGeneralEvaluation { get; set; }
        public int TravelAnnouncement { get; set; }
        public bool IsShared { get; set; }
        public string GroupTurProgram { get; set; }
        public string SuggestionComplaint { get; set; }
        public DateTime CrateionDate { get; set; } = DateTime.Now;
    }
}
