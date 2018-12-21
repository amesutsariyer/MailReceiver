using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailReceiver.Base.Entity
{
    public class Content
    {
        public string Header { get; set; }
        public string StrongSubHeader { get; set; }
        public string SubHeader { get; set; }
        public string Link { get; set; }
    }
}
