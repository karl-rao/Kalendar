using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients.Response
{
    [Serializable]
    [DataContract]
    public class MsonlineAnyone
    {
        [DataMember(Name = "emailAddress")]
        public MsonlineEmailaddress EmailAddress { get; set; }
    }
}
