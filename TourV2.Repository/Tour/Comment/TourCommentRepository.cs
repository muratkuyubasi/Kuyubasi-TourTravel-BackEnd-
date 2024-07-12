using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Domain;
using TourV2.Data;

namespace TourV2.Repository
{
    public class TourCommentRepository : GenericRepository<TourComment, TourContext>,
          ITourCommentRepository
    {
        public TourCommentRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}