using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Entities
{
    public class Mosque
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public virtual State State { get; set; }

    }
}
