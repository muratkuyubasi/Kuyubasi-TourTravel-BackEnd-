using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Domain;

namespace TourV2.Repository
{
    public class EmailSMTPSettingRepository : GenericRepository<EmailSMTPSetting, TourContext>,
           IEmailSMTPSettingRepository
    {
        public EmailSMTPSettingRepository(
            IUnitOfWork<TourContext> uow
            ) : base(uow)
        {
        }
    }
}
