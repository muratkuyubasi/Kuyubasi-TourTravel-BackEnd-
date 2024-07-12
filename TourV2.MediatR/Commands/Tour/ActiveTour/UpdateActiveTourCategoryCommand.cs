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
    public class UpdateActiveTourCategoryCommand : IRequest<ServiceResponse<TourCategory>>
    {
        public int Id { get; set; }
        public int ActiveTourId { get; set; }
        public int CategoryRecordId { get; set; }

    }
}
