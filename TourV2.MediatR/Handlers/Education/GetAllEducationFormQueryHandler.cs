using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.EducationForm;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Education
{
    public class GetAllEducationFormQueryHandler : IRequestHandler<GetAllEducationFormQuery,ServiceResponse<List<EducationFormDTO>>>
    {
        public readonly IEducationFormRepository _educationFormRepository;
        public readonly IMapper _mapper;

        public GetAllEducationFormQueryHandler(IEducationFormRepository EducationFormRepository, IMapper mapper)
        {
            _educationFormRepository = EducationFormRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<EducationFormDTO>>> Handle(GetAllEducationFormQuery request, CancellationToken cancellationToken)
        {
            var educationForms = await _educationFormRepository.All.Include(x=>x.Mosque).Include(x=>x.State).ToListAsync();
            return ServiceResponse<List<EducationFormDTO>>.ReturnResultWith200(_mapper.Map<List<EducationFormDTO>>(educationForms));
        }
    }
}
