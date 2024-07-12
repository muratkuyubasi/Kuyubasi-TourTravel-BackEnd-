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
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetPeriodQueryHandler : IRequestHandler<GetPeriodQuery, ServiceResponse<PeriodDto>>
    {
        private readonly IPeriodRepository _periodRepository;
        private readonly IMapper _mapper;

        public GetPeriodQueryHandler(IPeriodRepository periodRepository, IMapper mapper)
        {
            _periodRepository = periodRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PeriodDto>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
        {
            var entity = await _periodRepository.FindByInclude(x => x.Id == request.Id, i=>i.PeriodRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<PeriodDto>.ReturnResultWith200(_mapper.Map<PeriodDto>(entity));
            else
            {
                return ServiceResponse<PeriodDto>.Return404();
            }
        }
    }
}
