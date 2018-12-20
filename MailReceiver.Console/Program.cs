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
            var test =  MailReceiver.Base.MailService.SendMail();
        }
    }
}
