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
    public class MsonlineRecurrenceRange
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }


        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "recurrenceTimeZone")]
        public string RecurrenceTimeZone { get; set; }

        [DataMember(Name = "numberOfOccurrences")]
        public int NumberOfOccurrences { get; set; }
        
    }
}
