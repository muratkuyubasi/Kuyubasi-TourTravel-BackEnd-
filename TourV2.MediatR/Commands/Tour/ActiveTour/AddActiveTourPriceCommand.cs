using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddActiveTourPriceCommand : IRequest<ServiceResponse<TourPrice>>
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string Title { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public decimal ExtraPrice { get; set; }
        public int Discount { get; set; }
        public bool IsChildPrice { get; set; }

    }
}
