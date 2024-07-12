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
    public class GetAllActiveToursQueryHandler : IRequestHandler<GetAllActiveToursQuery, List<ActiveTourDto>>
    {
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly IMapper _mapper;
        public GetAllActiveToursQueryHandler(IActiveTourRepository activeTourRepository, IMapper mapper)
        {
            _activeTourRepository = activeTourRepository;
            _mapper = mapper;
        }

        public async Task<List<ActiveTourDto>> Handle(GetAllActiveToursQuery request, CancellationToken cancellationToken)
        {
            var entities = await _activeTourRepository
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
                    i => i.TourSpecifications,
                    i => i.TourCategories,
                    i => i.TourClicks
                ).Where(x=>x.TourRecord.LanguageCode== request.LanguageCode && !x.IsDeleted).ToListAsync();

            return _mapper.Map<List<ActiveTourDto>>(entities);

        }
    }
}
