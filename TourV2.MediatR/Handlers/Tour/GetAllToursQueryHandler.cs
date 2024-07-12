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
    public class GetAllToursQueryHandler : IRequestHandler<GetAllToursQuery, List<TourListDto>>
    {
        private readonly ITourRepository _tourRepository;
        public GetAllToursQueryHandler(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<List<TourListDto>> Handle(GetAllToursQuery request, CancellationToken cancellationToken)
        {
            var entities = await _tourRepository.AllIncluding(i => i.TourRecords)
                .Where(x=>x.TourRecords.Any(a=>a.LanguageCode == request.LanguageCode && x.IsActive && !x.IsDeleted))
                .Select(s => new TourListDto
                {
                    Id = s.Id,
                    IsActive = s.TourRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).IsActive,
                    TourRecordId = s.TourRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Id,
                    Code = s.TourRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Code,
                    Title = s.TourRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Title,
                    Slug = s.TourRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Slug,
                    LanguageCode = request.LanguageCode

                }).ToListAsync();
            return entities;
                
        }
    }
}
