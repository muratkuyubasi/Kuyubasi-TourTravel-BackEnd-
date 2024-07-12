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
    public class GetAllDeparturesQueryHandler : IRequestHandler<GetAllDeparturesQuery, List<DepartureListDto>>
    {
        private readonly IDepartureRepository _departureRepository;
        public GetAllDeparturesQueryHandler(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<List<DepartureListDto>> Handle(GetAllDeparturesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _departureRepository.AllIncluding(i => i.DepartureRecords)
                .Where(x=>x.DepartureRecords.Any(a=>a.LanguageCode == request.LanguageCode && x.IsActive))
                .Select(s => new DepartureListDto
                {
                    Id = s.Id,
                    IsActive = s.DepartureRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).IsActive,
                    DepartureRecordId = s.DepartureRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Id,
                    Code = s.DepartureRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Code,
                    Title = s.DepartureRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Title,
                    Slug = s.DepartureRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Slug,
                    LanguageCode = request.LanguageCode

                }).ToListAsync();
            return entities;
                
        }
    }
}
