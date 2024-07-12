using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    class GetLogQueryHandler : IRequestHandler<GetLogQuery, ServiceResponse<NLogDto>>
    {
        private readonly INLogRespository _nLogRespository;
        private readonly IMapper _mapper;

        public GetLogQueryHandler(
           INLogRespository nLogRespository,
           IMapper mapper)
        {
            _nLogRespository = nLogRespository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<NLogDto>> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {
            var entity = await _nLogRespository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<NLogDto>.ReturnResultWith200(_mapper.Map<NLogDto>(entity));
            else
                return ServiceResponse<NLogDto>.Return404();
        }
    }
}
