using System.Collections.Generic;
using System.Net;

namespace Kalendar.Zero.Utility.Common
{
    public class BrowerResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public Dictionary<string,string> Headers { get; set; } 
    }
}
