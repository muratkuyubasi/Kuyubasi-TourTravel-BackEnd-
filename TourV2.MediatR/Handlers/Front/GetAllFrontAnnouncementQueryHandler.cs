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
    public class GetAllFrontAnnouncementQueryHandler : IRequestHandler<GetAllFrontAnnouncementQuery, List<FrontAnnouncementDto>>
    {
        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllFrontAnnouncementQueryHandler> _logger;

        public GetAllFrontAnnouncementQueryHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            ILogger<GetAllFrontAnnouncementQueryHandler> logger)
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<FrontAnnouncementDto>> Handle(GetAllFrontAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontAnnouncementRepository.AllIncluding(x => x.FrontAnnouncementRecords).ToListAsync();
            return _mapper.Map<List<FrontAnnouncementDto>>(entities);
        }
    }
}
