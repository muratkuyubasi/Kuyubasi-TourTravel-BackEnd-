using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;

namespace TourV2.MediatR.Commands.State
{
    public class AddStateCommand : IRequest<ServiceResponse<StateDto>>
    {
        public string Name { get; set; }
    }
}
