using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTotalActiveTourCountQueryHandler : IRequestHandler<GetTotalActiveTourCountQuery, int>
    {
        private readonly IActiveTourRepository _activeTourRepository;
        public GetTotalActiveTourCountQueryHandler(IActiveTourRepository activeTourRepository)
        {
            _activeTourRepository = activeTourRepository;
        }
        public Task<int> Handle(GetTotalActiveTourCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_activeTourRepository.All.Where(x =>!x.IsDeleted).Count());
        }
    }
}
