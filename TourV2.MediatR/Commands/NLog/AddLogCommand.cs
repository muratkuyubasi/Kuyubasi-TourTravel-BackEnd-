using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddLogCommand : IRequest<ServiceResponse<NLogDto>>
    {
        public string ErrorMessage { get; set; }
        public string Stack { get; set; }
    }
}
