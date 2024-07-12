using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Entities;
using TourV2.Domain;

namespace TourV2.Repository
{
    public class CostCalculationRepository : GenericRepository<CostCalculation, TourContext>,
          ICostCalculationRepository
    {
        public CostCalculationRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}

