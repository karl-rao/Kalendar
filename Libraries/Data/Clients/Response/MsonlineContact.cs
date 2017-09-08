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
    public class MsonlineContact
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "homePhones")]
        public List<string> HomePhones { get; set; }

        [DataMember(Name = "mobilePhone")]
        public string MobilePhone { get; set; }
    }
}