using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;

namespace TourV2.MediatR.Commands.State
{
    public class DeleteStateCommand : IRequest<ServiceResponse<StateDto>>
    {
        public int Id { get; set; }

    }
}
