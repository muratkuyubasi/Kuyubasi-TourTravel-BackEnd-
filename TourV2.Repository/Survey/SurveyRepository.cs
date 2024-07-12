using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Entities;
using TourV2.Data.Entities.TourTravel;
using TourV2.Domain;

namespace TourV2.Repository.Survey
{
    public class SurveyRepository : GenericRepository<Data.Entities.TourTravel.Survey, TourContext>, ISurveyRepository
    {
        public SurveyRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {

        }
    }
}