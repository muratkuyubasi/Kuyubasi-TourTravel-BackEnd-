using System.Threading.Tasks;
using TourV2.Common.GenericRespository;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Data.Resources;

namespace TourV2.Repository
{
    public interface ILoginAuditRepository : IGenericRepository<LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
