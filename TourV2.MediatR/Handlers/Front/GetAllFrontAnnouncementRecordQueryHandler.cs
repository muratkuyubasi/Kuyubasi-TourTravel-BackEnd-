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
    public class GetAllFrontAnnouncementRecordQueryHandler : IRequestHandler<GetAllFrontAnnouncementRecordQuery, List<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllFrontAnnouncementRecordQueryHandler> _logger;

        public GetAllFrontAnnouncementRecordQueryHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            ILogger<GetAllFrontAnnouncementRecordQueryHandler> logger)
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<FrontAnnouncementRecordDto>> Handle(GetAllFrontAnnouncementRecordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _FrontAnnouncementRecordRepository.AllIncluding(x => x.FrontAnnouncement).ToListAsync();
            return _mapper.Map<List<FrontAnnouncementRecordDto>>(entities);
        }
    }
}
