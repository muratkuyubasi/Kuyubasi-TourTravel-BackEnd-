using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateAllCostCalculationCommand : IRequest<ServiceResponse<CostCalculationDto>>
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string RoomNumber { get; set; }
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
    }
}