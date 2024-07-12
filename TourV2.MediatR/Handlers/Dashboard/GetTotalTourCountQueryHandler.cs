using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTotalTourCountQueryHandler : IRequestHandler<GetTotalTourCountQuery, int>
    {
        private readonly ITourRepository _tourRepository;
        public GetTotalTourCountQueryHandler(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }
        public Task<int> Handle(GetTotalTourCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_tourRepository.All.Where(x =>  !x.IsDeleted).Count());
        }
    }
}
