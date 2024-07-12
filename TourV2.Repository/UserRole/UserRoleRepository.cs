using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Domain;

namespace TourV2.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole, TourContext>,
       IUserRoleRepository
    {
        public UserRoleRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {
        }
    }
}
