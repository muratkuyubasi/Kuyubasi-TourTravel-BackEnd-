using AutoMapper;
using MediatR;
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
using TourV2.MediatR.Queries.PeriodEducation;
using TourV2.Repository;
using TourV2.Repository.PeriodEducation;

namespace TourV2.MediatR.Handlers.PeriodEducation
{
    public class GetPeriodEducationQueryHandler : IRequestHandler<GetPeriodEducationQuery, ServiceResponse<PeriodEducationDto>>
    {
        private readonly IPeriodEducationRepository _periodEducationRepository;
        private readonly IMapper _mapper;

        public GetPeriodEducationQueryHandler(IPeriodEducationRepository PeriodEducationRepository,
            IMapper mapper)
        {
            _periodEducationRepository = PeriodEducationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PeriodEducationDto>> Handle(GetPeriodEducationQuery request, CancellationToken cancellationToken)
        {
            var periodEducation = _periodEducationRepository.FindBy( x=> x.Id == request.Id).FirstOrDefault();
            if (periodEducation != null)
            {
                return ServiceResponse<PeriodEducationDto>.ReturnResultWith200(_mapper.Map<PeriodEducationDto>(periodEducation));
            }
            else
            {
                return ServiceResponse<PeriodEducationDto>.Return404();
            }
        }
    }
}
