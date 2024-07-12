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
    public class GetFrontAnnouncementQueryHandler : IRequestHandler<GetFrontAnnouncementQuery, ServiceResponse<FrontAnnouncementDto>>
    {

        private readonly IFrontAnnouncementRepository _FrontAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFrontAnnouncementQueryHandler> _logger;

        public GetFrontAnnouncementQueryHandler(
           IFrontAnnouncementRepository FrontAnnouncementRepository,
            IMapper mapper,
            ILogger<GetFrontAnnouncementQueryHandler> logger)
        {
            _FrontAnnouncementRepository = FrontAnnouncementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementDto>> Handle(GetFrontAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontAnnouncementRepository.AllIncluding(c => c.FrontAnnouncementRecords)
                .Where(c => c.Code == request.Code)
                .FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontAnnouncementDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<FrontAnnouncementDto>.Return404();
            }
        }
    }
}
