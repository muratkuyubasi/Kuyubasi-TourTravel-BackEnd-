using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetActiveUserCountQueryHandler : IRequestHandler<GetActiveUserCountQuery, int>
    {
        private readonly IUserRepository _userRepository;
        public GetActiveUserCountQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<int> Handle(GetActiveUserCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.All.Where(c => c.IsActive).Count());
        }
    }
}
