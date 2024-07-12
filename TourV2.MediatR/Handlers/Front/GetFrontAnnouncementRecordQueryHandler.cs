using AutoMapper;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TourV2.Helper;

namespace TourV2.MediatR.Handlers
{
    public class GetFrontAnnouncementRecordQueryHandler : IRequestHandler<GetFrontAnnouncementRecordQuery, ServiceResponse<FrontAnnouncementRecordDto>>
    {

        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFrontAnnouncementRecordQueryHandler> _logger;

        public GetFrontAnnouncementRecordQueryHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            ILogger<GetFrontAnnouncementRecordQueryHandler> logger)
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(GetFrontAnnouncementRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontAnnouncementRecordRepository.AllIncluding(x => x.FrontAnnouncement)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementRecordDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<FrontAnnouncementRecordDto>.Return404();
            }
        }
    }
}
