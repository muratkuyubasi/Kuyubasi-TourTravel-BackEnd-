using MediatR;
using TourV2.Data.Resources;
using TourV2.Repository;

namespace TourV2.MediatR.Queries
{
    public class GetAllLoginAuditQuery : IRequest<LoginAuditList>
    {
        public LoginAuditResource LoginAuditResource { get; set; }
    }
}
