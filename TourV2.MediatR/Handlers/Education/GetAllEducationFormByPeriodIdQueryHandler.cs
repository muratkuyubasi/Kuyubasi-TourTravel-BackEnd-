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
using TourV2.MediatR.Queries.EducationForm;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Education
{
    public class GetAllEducationFormByPeriodIdQueryHandler : IRequestHandler<GetAllEducationFormByPeriodIdQuery, ServiceResponse<List<EducationFormDTO>>>
    {
        public readonly IEducationFormRepository _educationFormRepository;
        public readonly IMapper _mapper;

        public GetAllEducationFormByPeriodIdQueryHandler(IEducationFormRepository EducationFormRepository, IMapper mapper)
        {
            _educationFormRepository = EducationFormRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<EducationFormDTO>>> Handle(GetAllEducationFormByPeriodIdQuery request, CancellationToken cancellationToken)
        {
            var educationForms = await _educationFormRepository.All.Include(x =>x.State).Include(x => x.Mosque).Where(x =>x.PeriodEducationId ==request.PeriodId).ToListAsync();
            return ServiceResponse<List<EducationFormDTO>>.ReturnResultWith200(_mapper.Map<List<EducationFormDTO>>(educationForms));
        }
    }
}
