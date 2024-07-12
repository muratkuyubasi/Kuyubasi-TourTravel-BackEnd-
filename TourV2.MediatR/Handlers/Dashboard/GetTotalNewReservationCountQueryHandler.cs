using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTotalNewReservationCountQueryHandler : IRequestHandler<GetTotalNewReservationCountQuery, int>
    {
        private readonly ITourReservationRepository _reservationRepository;
        public GetTotalNewReservationCountQueryHandler(ITourReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public Task<int> Handle(GetTotalNewReservationCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_reservationRepository.All.Where(x=>!x.IsCompleted && !x.IsDeleted).Count());
        }
    }
}
