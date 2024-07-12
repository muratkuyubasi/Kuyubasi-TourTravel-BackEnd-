using System.Threading.Tasks;
using TourV2.Common.GenericRespository;
using TourV2.Data;
using TourV2.Data.Resources;

namespace TourV2.Repository
{
    public interface INLogRespository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
