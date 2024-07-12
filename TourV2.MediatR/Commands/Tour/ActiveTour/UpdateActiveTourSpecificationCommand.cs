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
    public class UpdateActiveTourSpecificationCommand : IRequest<ServiceResponse<TourSpecification>>
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public string Specification { get; set; }
        public bool InPrice { get; set; }

    }
}
