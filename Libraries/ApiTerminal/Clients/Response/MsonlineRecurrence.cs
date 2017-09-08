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
    public class MsonlineRecurrence
    {

        [DataMember(Name = "pattern")]
        public MsonlineRecurrencePattern Pattern { get; set; }


        [DataMember(Name = "range")]
        public MsonlineRecurrenceRange Range { get; set; }
    }
}
