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
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllActiveToursSearchQueryHandler : IRequestHandler<GetAllActiveToursSearchQuery, List<ActiveTourDto>>
    {
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly IMapper _mapper;
        public GetAllActiveToursSearchQueryHandler(IActiveTourRepository activeTourRepository, IMapper mapper)
        {
            _activeTourRepository = activeTourRepository;
            _mapper = mapper;
        }

        public async Task<List<ActiveTourDto>> Handle(GetAllActiveToursSearchQuery request, CancellationToken cancellationToken)
        {
            var entities = _activeTourRepository
                .AllIncluding(
                    i => i.TourRecord,
                    i => i.TourCategories,
                    i => i.TourDepartures,
                    i => i.TourDays,
                    i => i.TourTransportation,
                    i => i.TourMedias,
                    i => i.TourPrices,
                    i => i.PeriodRecord,
                    i => i.RegionRecord,
                    i => i.TourSpecifications
                ).Include(x => x.TourCategories).ThenInclude(x => x.CategoryRecord).Where(x => x.TourRecord.LanguageCode == request.LanguageCode && !x.IsDeleted);

            if (!string.IsNullOrEmpty(request.TourName))
            {
                entities = entities.Where(x => x.TourRecord.Title.Contains(request.TourName));
            }

            if (!string.IsNullOrEmpty(request.PeriodName))
            {
                entities = entities.Where(x => x.PeriodRecord.Title.Contains(request.PeriodName));
            }

            if (!string.IsNullOrEmpty(request.RegionName))
            {
                entities = entities.Where(x => x.RegionRecord.Title.Contains(request.RegionName));
            }

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                entities = entities.Where(x => x.TourCategories.Any(y => y.CategoryRecord.Title.Contains(request.CategoryName)));
            }

            if (!string.IsNullOrEmpty(request.DepartureName))
            {
                entities = entities.Where(x => x.TourDepartures.Any(y => y.DepartureRecord.Title.Contains(request.DepartureName)));
            }

            if (!string.IsNullOrEmpty(request.TransportationName))
            {
                entities = entities.Where(x => x.TourTransportation.Title.Contains(request.TransportationName));
            }

            if (request.StartDate != null)
            {
                entities = entities.Where(x => x.StartDate >= request.StartDate);
            }

            if (request.EndDate != null)
            {
                entities = entities.Where(x => x.EndDate <= request.EndDate);
            }

            if (request.FinishDate != null)
            {
                entities = entities.Where(x => x.FinishDate <= request.FinishDate);
            }

            if (request.MinPrice != null)
            {
                entities = entities.Where(x => x.TourPrices.FirstOrDefault(y => !y.IsChildPrice).Price >= request.MinPrice);
            }

            if (request.MaxPrice != null)
            {
                entities = entities.Where(x => x.TourPrices.FirstOrDefault(y => !y.IsChildPrice).Price <= request.MaxPrice);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                entities = entities.Where(x => x.TourRecord.Title.Contains(request.SearchTerm) ||
                                    x.PeriodRecord.Title.Contains(request.SearchTerm) ||
                                    x.RegionRecord.Title.Contains(request.SearchTerm) ||
                                    x.TourCategories.Any(y => y.CategoryRecord.Title.Contains(request.SearchTerm)) ||
                                    x.Description.Contains(request.SearchTerm) ||
                                    x.ShortDescription.Contains(request.SearchTerm) ||
                                    x.TourDepartures.Any(y => y.DepartureRecord.Title.Contains(request.SearchTerm)) ||
                                    x.TourTransportation.Title.Contains(request.SearchTerm));
            }

            var activeTours = await entities.ToListAsync();

            return _mapper.Map<List<ActiveTourDto>>(activeTours);

        }
    }
}
