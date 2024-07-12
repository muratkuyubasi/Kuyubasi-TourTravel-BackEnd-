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
    public class GetAllPeriodsQueryHandler : IRequestHandler<GetAllPeriodsQuery, List<PeriodListDto>>
    {
        private readonly IPeriodRepository _periodRepository;
        public GetAllPeriodsQueryHandler(IPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;
        }

        public async Task<List<PeriodListDto>> Handle(GetAllPeriodsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _periodRepository.AllIncluding(i => i.PeriodRecords)
                .Where(x=>x.PeriodRecords.Any(a=>a.LanguageCode == request.LanguageCode && x.IsActive))
                .Select(s => new PeriodListDto
                {
                    Id = s.Id,
                    IsActive = s.PeriodRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).IsActive,
                    PeriodRecordId = s.PeriodRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Id,
                    Code = s.PeriodRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Code,
                    Title = s.PeriodRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Title,
                    Slug = s.PeriodRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Slug,
                    LanguageCode = request.LanguageCode

                }).ToListAsync();
            return entities;
                
        }
    }
}
