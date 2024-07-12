using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTotalReservationCountQueryHandler : IRequestHandler<GetTotalReservationCountQuery, int>
    {
        private readonly ITourReservationRepository _reservationRepository;
        public GetTotalReservationCountQueryHandler(ITourReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public Task<int> Handle(GetTotalReservationCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_reservationRepository.All.Where(x => !x.IsDeleted).Count());
        }
    }
}
