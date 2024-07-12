using MediatR;
using TourV2.Data.Resources;
using TourV2.Repository;

namespace TourV2.MediatR.Queries
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
