using AutoMapper;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class GetPageQueryHandler : IRequestHandler<GetPageQuery, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPageQueryHandler> _logger;

        public GetPageQueryHandler(
            IPageRepository pageRepository,
            IMapper mapper,
            ILogger<GetPageQueryHandler> logger)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<ServiceResponse<PageDto>> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pageRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<PageDto>.Return404();
            }
        }
    }
}
