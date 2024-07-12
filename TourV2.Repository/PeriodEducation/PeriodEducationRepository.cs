using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Domain;

namespace TourV2.Repository.PeriodEducation
{
    public class PeriodEducationRepository : GenericRepository<Data.Entities.PeriodEducation, TourContext>,
          IPeriodEducationRepository
    {
        public PeriodEducationRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}