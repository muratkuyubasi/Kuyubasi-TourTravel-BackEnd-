using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class SendEmailCommand : IRequest<ServiceResponse<EmailDto>>
    {
        public string Subject { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public List<FileInfo> Attechments { get; set; } = new List<FileInfo>();
        public string Body { get; set; }
        public string FromAddress { get; set; }
    }

}
