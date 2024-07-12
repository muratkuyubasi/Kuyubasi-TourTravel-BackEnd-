using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Domain;

namespace TourV2.Repository
{
    public class NewsletterSubscriptionRepository : GenericRepository<NewsletterSubscription, TourContext>,
          INewsletterSubscriptionRepository
    {
        public NewsletterSubscriptionRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}