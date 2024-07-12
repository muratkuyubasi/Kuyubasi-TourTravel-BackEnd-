using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data
{
    public class TourPrice
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public decimal ExtraPrice { get; set; }
        public int Discount { get; set; }
        public bool IsChildPrice { get; set; }
        
    }
}
