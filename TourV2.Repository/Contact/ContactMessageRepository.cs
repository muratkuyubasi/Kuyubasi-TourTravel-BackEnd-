using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Domain;

namespace TourV2.Repository
{
    public class ContactMessageRepository : GenericRepository<ContactMessage, TourContext>,
          IContactMessageRepository
    {
        public ContactMessageRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}