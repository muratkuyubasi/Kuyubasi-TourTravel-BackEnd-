using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetNLogsQueryHandler : IRequestHandler<GetNLogsQuery, NLogList>
    {
        private readonly INLogRespository _nLogRespository;
        public GetNLogsQueryHandler(INLogRespository nLogRespository)
        {
            _nLogRespository = nLogRespository;
        }
        public async Task<NLogList> Handle(GetNLogsQuery request, CancellationToken cancellationToken)
        {
            return await _nLogRespository.GetNLogsAsync(request.NLogResource);
        }
    }
}
