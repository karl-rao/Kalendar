using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data.Clients.Response
{
    [Serializable]
    [DataContract]
    public class MsonlineLocation
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
