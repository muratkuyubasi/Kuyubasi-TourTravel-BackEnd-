using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Mosque> Mosques { get; set; }
    }
}