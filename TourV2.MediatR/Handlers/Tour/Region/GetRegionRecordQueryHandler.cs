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
    public class GetRegionRecordQueryHandler: IRequestHandler<GetRegionRecordQuery, ServiceResponse<RegionRecordDto>>
    {
        private readonly IRegionRecordRepository _regionRecordRepository;
        private readonly IMapper _mapper;

        public GetRegionRecordQueryHandler(IRegionRecordRepository regionRecordRepository, IMapper mapper)
        {
            _regionRecordRepository = regionRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RegionRecordDto>> Handle(GetRegionRecordQuery request ,CancellationToken cancellationToken)
        {
            var entity = await _regionRecordRepository.FindBy(x => x.Code == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<RegionRecordDto>.ReturnResultWith200(_mapper.Map<RegionRecordDto>(entity));
            else
            {
                return ServiceResponse<RegionRecordDto>.Return404();
            }
        }
    }
}
