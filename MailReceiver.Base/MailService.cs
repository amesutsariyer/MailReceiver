using MailReceiver.Base.Entity;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MailReceiver.Base
{

    public static class MailService
    {
        public static Response SendMail()
        {
            Response response = new Response();
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "info@ahmetsariyer.com",
                        Password = "2$qzV62o"
                    };
                    client.Credentials = credential;
                    client.Host = "ahmetsariyer.com";
                    client.Port = 587;
                    client.EnableSsl = false;
                    var message = GetMailWithImg();
                    client.Send(message);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        private static AlternateView GetEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }
        private static MailMessage GetMailWithOutImg()
        {
            var emailMessage = new MailMessage();
            emailMessage.To.Add(new MailAddress("ahmetmesutsariyer@gmail.com"));
            emailMessage.From = new MailAddress("info@ahmetsariyer.com");
            emailMessage.Subject = "Subsctiption Test";
            emailMessage.Body = "Selamın Aleyküm";
            emailMessage.IsBodyHtml = true;
            return emailMessage;
        }
        private static MailMessage GetMailWithImg()
        {
            var emailMessage = new MailMessage();
            emailMessage.AlternateViews.Add(GetEmbeddedImage(Path.Combine(Environment.CurrentDirectory, @"MailDownloadFile", "download.png")));
            emailMessage.To.Add(new MailAddress("ahmetmesutsariyer@gmail.com"));
            emailMessage.From = new MailAddress("info@ahmetsariyer.com");
            emailMessage.Subject = "Subsctiption Test";
            emailMessage.IsBodyHtml = true;
            return emailMessage;
        }
    }
}
