using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddAppSettingCommand: IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
