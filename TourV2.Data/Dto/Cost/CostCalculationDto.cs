using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Helper;

namespace TourV2.Data.Dto
{
    public class CostCalculationDto
    {
        public int Id { get; set; }
        public int No { get; set; }
        public DateTime TourStartDate { get; set; }
        public DateTime TourEndDate { get; set; }
        public string RoomNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public RoomType RoomType { get; set; }
        public decimal GelenHavale { get; set; }
        public decimal DitibDestek { get; set; }
        public decimal SatisFiyati { get; set; }
        public decimal Maliyet { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal MaliyetToplam { get; set; }
        public decimal TurKar { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int ActiveTourId { get; set; }
        public ActiveTourDto ActiveTour { get; set; }
    }
}
