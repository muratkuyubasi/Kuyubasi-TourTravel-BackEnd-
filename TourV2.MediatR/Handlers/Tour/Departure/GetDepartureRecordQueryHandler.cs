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
    public class GetDepartureRecordQueryHandler: IRequestHandler<GetDepartureRecordQuery, ServiceResponse<DepartureRecordDto>>
    {
        private readonly IDepartureRecordRepository _departureRecordRepository;
        private readonly IMapper _mapper;

        public GetDepartureRecordQueryHandler(IDepartureRecordRepository departureRecordRepository, IMapper mapper)
        {
            _departureRecordRepository = departureRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DepartureRecordDto>> Handle(GetDepartureRecordQuery request ,CancellationToken cancellationToken)
        {
            var entity = await _departureRecordRepository.FindByInclude(x => x.Code == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<DepartureRecordDto>.ReturnResultWith200(_mapper.Map<DepartureRecordDto>(entity));
            else
            {
                return ServiceResponse<DepartureRecordDto>.Return404();
            }
        }
    }
}
