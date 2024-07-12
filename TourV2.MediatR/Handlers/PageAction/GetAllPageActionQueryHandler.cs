using AutoMapper;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TourV2.MediatR.Handlers
{
    public class GetAllPageActionQueryHandler : IRequestHandler<GetAllPageActionQuery, List<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPageActionQueryHandler> _logger;

        public GetAllPageActionQueryHandler(
          IPageActionRepository pageActionRepository,
            IMapper mapper,
            ILogger<GetAllPageActionQueryHandler> logger)
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PageActionDto>> Handle(GetAllPageActionQuery request, CancellationToken cancellationToken)
        {
            var entities = await _pageActionRepository.All.ToListAsync();
            return _mapper.Map<List<PageActionDto>>(entities);
        }
    }
}
