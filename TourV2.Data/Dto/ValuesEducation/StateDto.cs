using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Entities;

namespace TourV2.Data.Dto.ValuesEducation
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Mosque> Mosques { get; set; }

    }
}
