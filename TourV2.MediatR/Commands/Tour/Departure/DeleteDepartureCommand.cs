using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeleteDepartureCommand : IRequest<ServiceResponse<DepartureDto>>
    {
        public int Id { get; set; }
    }
}
