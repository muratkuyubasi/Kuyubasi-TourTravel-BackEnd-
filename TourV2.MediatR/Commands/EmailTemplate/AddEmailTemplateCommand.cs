using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
