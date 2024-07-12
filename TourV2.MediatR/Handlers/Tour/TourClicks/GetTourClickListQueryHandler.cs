using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTourClickListQueryHandler: IRequestHandler<GetTourClickCountQuery, ServiceResponse<int>>
    {
        public readonly ITourClickRepository _tourClickRepository;
        public readonly IMapper _mapper;

        public GetTourClickListQueryHandler(ITourClickRepository tourClickRepository, IMapper mapper)
        {
            _tourClickRepository = tourClickRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> Handle(GetTourClickCountQuery request, CancellationToken cancellationToken)
        {
            var tourClicks = await _tourClickRepository.All.ToListAsync();
            if (request.ActiveTourId > 0)
                tourClicks = tourClicks.Where(x => x.ActiveTourId == request.ActiveTourId).ToList();
            return ServiceResponse<int>.ReturnResultWith200(tourClicks.Count());
        }
    }
}
