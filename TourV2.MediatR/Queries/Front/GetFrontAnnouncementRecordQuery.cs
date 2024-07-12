using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetFrontAnnouncementRecordQuery : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int Id { get; set; }
    }
}
