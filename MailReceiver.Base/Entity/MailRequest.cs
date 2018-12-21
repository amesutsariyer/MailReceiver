using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailReceiver.Base.Entity
{

    public class MailRequest
    {
        public List<string> ToArray { get; set; }
        public List<string> CcArray { get; set; }
        public List<string> BccArray { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
       
        public string From { get; set; }
        public int Port { get; set; }
        public string HostName { get; set; }
        public Sender Sender { get; set; }
        public Credential Credential { get; set; }
        public Content Content { get; set; }
    }

}
