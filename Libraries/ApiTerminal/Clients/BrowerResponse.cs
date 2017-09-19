using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public class BrowerResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public Dictionary<string,string> Headers { get; set; } 
    }
}
