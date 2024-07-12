using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto.Tour;
using TourV2.Helper;

namespace TourV2.MediatR.Commands.Survery
{
    public class AddSurveryCommand : IRequest<ServiceResponse<SurveyDto>>
    {
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
    }
}
