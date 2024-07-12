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
    public class GetPeriodRecordQueryHandler: IRequestHandler<GetPeriodRecordQuery, ServiceResponse<PeriodRecordDto>>
    {
        private readonly IPeriodRecordRepository _periodRecordRepository;
        private readonly IMapper _mapper;

        public GetPeriodRecordQueryHandler(IPeriodRecordRepository periodRecordRepository, IMapper mapper)
        {
            _periodRecordRepository = periodRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PeriodRecordDto>> Handle(GetPeriodRecordQuery request ,CancellationToken cancellationToken)
        {
            var entity = await _periodRecordRepository.FindBy(x => x.Code == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<PeriodRecordDto>.ReturnResultWith200(_mapper.Map<PeriodRecordDto>(entity));
            else
            {
                return ServiceResponse<PeriodRecordDto>.Return404();
            }
        }
    }
}
