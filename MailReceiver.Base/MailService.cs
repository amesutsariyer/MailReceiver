using MailReceiver.Base.Entity;
using System;
using System.Net;
using System.Net.Mail;
namespace MailReceiver.Base
{

    public class MailService
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
                        UserName = "",
                        Password = ""
                    };
                    client.Credentials = credential;
                    client.Host = "";
                    client.Port = 587;
                    client.EnableSsl = false;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(""));

                        emailMessage.From = new MailAddress("");
                        emailMessage.Subject = "Subsctiption Test";
                        emailMessage.Body = "";
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
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
    }
}
