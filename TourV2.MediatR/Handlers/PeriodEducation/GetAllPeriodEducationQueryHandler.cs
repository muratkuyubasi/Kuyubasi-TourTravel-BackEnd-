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
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.EducationForm;
using TourV2.MediatR.Queries.PeriodEducation;
using TourV2.Repository;
using TourV2.Repository.PeriodEducation;

namespace TourV2.MediatR.Handlers.PeriodEducation
{
    public class GetAllPeriodEducationQueryHandler : IRequestHandler<GetAllPeriodEducationQuery, ServiceResponse<List<PeriodEducationDto>>>
    {
        public readonly IPeriodEducationRepository _periodEducationRepository;
        public readonly IMapper _mapper;

        public GetAllPeriodEducationQueryHandler(IPeriodEducationRepository PeriodEducationRepository, IMapper mapper)
        {
            _periodEducationRepository = PeriodEducationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<PeriodEducationDto>>> Handle(GetAllPeriodEducationQuery request, CancellationToken cancellationToken)
        {
            var periodEducations = await _periodEducationRepository.All.ToListAsync();
            return ServiceResponse<List<PeriodEducationDto>>.ReturnResultWith200(_mapper.Map<List<PeriodEducationDto>>(periodEducations));
        }
    }
}
