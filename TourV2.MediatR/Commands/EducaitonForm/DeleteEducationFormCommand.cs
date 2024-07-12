using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;

namespace TourV2.MediatR.Commands.EducaitonForm
{
    public class DeleteEducationFormCommand : IRequest<ServiceResponse<EducationFormDTO>>
    {
        public int Id { get; set; }
    }
}
