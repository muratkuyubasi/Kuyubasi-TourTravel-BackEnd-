using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetAppSettingByKeyQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Key { get; set; }
    }
}
