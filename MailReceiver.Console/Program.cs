using MailReceiver.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailReceiver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var toArray = new List<string>();
            toArray.Add("ahmetmesutsariyer@gmail.com");
            toArray.Add("amesutsariyer@outlook.com");
            var model = new MailRequest()
            {
                Credential = new Credential()
                {
                    UserName = "info@ahmetsariyer.com",
                    Password = ""
                },
                HostName = "ahmetsariyer.com",
                Port=587,
                Subject = "Üsküdar Denetim yeminli Mali Müşavirlik",
                From ="info@ahmetsariyer.com",
                ToArray= toArray,
                Content = new Content()
                {
                    Header = "2018 Yılı İçin Yeniden Değerleme Oranı",
                    Link = "http://213.128.89.156/plesk-site-preview/uskudardenetim.com/Circular/Detail/8effacc6-3912-457b-98dd-e89a8d90300f",
                    StrongSubHeader ="Üsküdar Denetim",
                    SubHeader = "Yayınlamış olan bu sirküyü okumak için lütfen bizi ziyaret edin."
                }
            };
            var test =  MailReceiver.Base.MailService.SendMail(model);
        }
    }
}
