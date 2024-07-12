using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using MailKit.Security;

namespace TourV2.Helper
{
    public class EmailHelper
    {
        static List<MemoryStream> attachments = new List<MemoryStream>();
        public static void SendEmail(SendEmailSpecification sendEmailSpecification)
        {
            MailMessage message = new MailMessage()
            {
                Sender = new MailAddress(sendEmailSpecification.FromAddress),
                From = new MailAddress(sendEmailSpecification.FromAddress),
                Subject = sendEmailSpecification.Subject,
                IsBodyHtml = true,
                Body = sendEmailSpecification.Body,
            };

            if (sendEmailSpecification.Attachment != null)
            {
                message.Attachments.Add(sendEmailSpecification.Attachment);
            }

            if (sendEmailSpecification.Attachments != null)
            {
                foreach (var item in sendEmailSpecification.Attachments)
                {
                    message.Attachments.Add(item);
                }
            }

            if (sendEmailSpecification.Attechments?.Count > 0)
            {
                Attachment attach;
                foreach (var file in sendEmailSpecification.Attechments)
                {

                    string fileData = file.Src.Split(',').LastOrDefault();
                    byte[] bytes = Convert.FromBase64String(fileData);
                    var ms = new MemoryStream(bytes);
                    attach = new Attachment(ms, file.Name, file.FileType);
                    attachments.Add(ms);
                    message.Attachments.Add(attach);
                }
            }
            sendEmailSpecification.ToAddress.Split(",").ToList().ForEach(toAddress =>
            {
                message.To.Add(new MailAddress(toAddress));
            });
            if (!string.IsNullOrEmpty(sendEmailSpecification.CCAddress))
            {
                sendEmailSpecification.CCAddress.Split(",").ToList().ForEach(ccAddress =>
                {
                    message.CC.Add(new MailAddress(ccAddress));
                });
            }

            SmtpClient smtp = new SmtpClient()
            {
                Port = sendEmailSpecification.Port,
                Host = sendEmailSpecification.Host,
                EnableSsl = sendEmailSpecification.IsEnableSSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sendEmailSpecification.UserName, sendEmailSpecification.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtp.Send(message);
            if (attachments.Count() > 0)
            {
                foreach (var attachment in attachments)
                {
                    try
                    {
                        attachment.Dispose();
                    }
                    catch
                    {
                    }
                }
            }

        }

        private static Attachment ConvertStringToStream(FileInfo fileInfo)
        {
            string fileData = fileInfo.Src.Split(',').LastOrDefault();
            byte[] bytes = Convert.FromBase64String(fileData);
            System.Net.Mail.Attachment attach;
            using (MemoryStream ms = new MemoryStream(bytes))
            {

                attach = new System.Net.Mail.Attachment(ms, fileInfo.Name, fileInfo.FileType);
                // I guess you know how to send email with an attachment
                // after sending email
                //ms.Close();
                attachments.Add(ms);
            }
            return attach;
        }

        public class MailResponse
        {
            public string Message { get; set; }
            public bool Result { get; set; }
        }
        public static MailResponse SendMailByMailKit(string message, string subject, string mail)
        {
            /*
             https://copyprogramming.com/howto/send-mailkit-email-with-an-attachment-from-memorystream
             https://www.taithienbo.com/send-email-with-attachments-using-mailkit-for-net-core/
             https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit
             */
            var response = new MailResponse();
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse("rezervasyon@zsureisen.eu"));
                emailMessage.To.Add(MailboxAddress.Parse(mail));
                emailMessage.Cc.Add(MailboxAddress.Parse("rezervasyon@zsureisen.eu"));
                emailMessage.Subject = subject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = message;
                #region HTML
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message};
                #endregion

                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("w01dfe94.kasserver.com", 25, SecureSocketOptions.Auto);
                smtp.Authenticate("rezervasyon@zsureisen.eu", "RzEdrn10#");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
                response.Message = "E-Posta gönderme işlemi başarılı!";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "E-Posta Gönderme işlemi sırasında bir hata meydana geldi; " + ex.Message;
                response.Result = false;
                return response;
            }

        }

        #region MailKit pdf
        //public static MailResponse SendMailByMailKitWithDocument(string message, string subject, string mail,List<Attachment> attachmentsList, string fileName)
        //{
        //    /*
        //     https://copyprogramming.com/howto/send-mailkit-email-with-an-attachment-from-memorystream
        //     https://www.taithienbo.com/send-email-with-attachments-using-mailkit-for-net-core/
        //     https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit
        //     */
        //    var response = new MailResponse();
        //    try
        //    {
        //        MimeMessage emailMessage = new MimeMessage();
        //        emailMessage.From.Add(MailboxAddress.Parse("rezervasyon@zsureisen.eu"));
        //        emailMessage.To.Add(MailboxAddress.Parse(mail));
        //        emailMessage.Subject = subject;

        //        Multipart multipart = new Multipart("mixed");

        //        BodyBuilder emailBodyBuilder = new BodyBuilder();
        //        emailBodyBuilder.TextBody = message;

        //        #region HTML
        //        var htmlPart = new TextPart(TextFormat.Html)
        //        {
        //            Text = message };
        //        #endregion

        //        var multipart2 = new Multipart("related");
        //        multipart2.Add(htmlPart);
        //        multipart2.Add(emailBodyBuilder.ToMessageBody());

        //        if (attachmentsList != null)
        //        {
        //            foreach(var al in attachmentsList)
        //            {
        //                var attachment = new MimePart("application", "pdf")
        //                {
        //                    Content = new MimeContent(new MemoryStream(file), ContentEncoding.Default),
        //                    ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
        //                    ContentTransferEncoding = ContentEncoding.Base64,
        //                    FileName = fileName
        //                };

        //                multipart2.Add(attachment);
        //            }
        //        }
        //        multipart.Add(multipart2);

        //        emailMessage.Body = multipart;

        //        var smtp = new MailKit.Net.Smtp.SmtpClient();
        //        smtp.Connect("w01dfe94.kasserver.com", 25, SecureSocketOptions.Auto);
        //        smtp.Authenticate("rezervasyon@zsureisen.eu", "RzEdrn10#");
        //        smtp.Send(emailMessage);
        //        smtp.Disconnect(true);
        //        response.Message = "E-Posta gönderme işlemi başarılı!";
        //        response.Result = true;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "E-Posta Gönderme işlemi sırasında bir hata meydana geldi; " + ex.Message;
        //        response.Result = false;
        //        return response;
        //    }

        //}
        #endregion

    }
}
