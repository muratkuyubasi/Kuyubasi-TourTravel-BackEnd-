using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, List<RegionListDto>>
    {
        private readonly IRegionRepository _regionRepository;
        public GetAllRegionsQueryHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<List<RegionListDto>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _regionRepository.AllIncluding(i => i.RegionRecords)
                .Where(x=>x.RegionRecords.Any(a=>a.LanguageCode == request.LanguageCode && x.IsActive))
                .Select(s => new RegionListDto
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    RegionRecordId = s.RegionRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Id,
                    Code = s.RegionRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Code,
                    Title = s.RegionRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Title,
                    Slug = s.RegionRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Slug,
                    LanguageCode = request.LanguageCode

                }).ToListAsync();
            return entities;
                
        }
    }
}
