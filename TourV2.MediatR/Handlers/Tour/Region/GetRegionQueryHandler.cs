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
    public class GetRegionQueryHandler : IRequestHandler<GetRegionQuery, ServiceResponse<RegionDto>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public GetRegionQueryHandler(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RegionDto>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _regionRepository.FindByInclude(x => x.Id == request.Id, i=>i.RegionRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<RegionDto>.ReturnResultWith200(_mapper.Map<RegionDto>(entity));
            else
            {
                return ServiceResponse<RegionDto>.Return404();
            }
        }
    }
}
