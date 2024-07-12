using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeleteFrontAnnouncementRecordCommand : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int Id { get; set; }
    }
}
