using System.Collections.Generic;
using System.Net.Mail;

namespace TourV2.Helper
{
    public class SendEmailSpecification
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool IsEnableSSL { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? isAttechment { get; set; }
        public Attachment Attachment { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<FileInfo> Attechments { get; set; }


    }
}
