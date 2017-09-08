using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Kalendar.Zero.Data.Entities
{
    [Serializable]
    [DataContract]
    public class KalendarEvent
    {
        [DataMember(Name = "id")]
        public int EventId { get; set; }

        [DataMember(Name = "title")]
        public string EventTitle { get; set; }

        [DataMember(Name = "url")]
        public string EventUrl { get; set; }

        [DataMember(Name = "class")]
        public string EventClass{get;set; }

        [DataMember(Name = "className")]
        public string EventClassName { get; set; }

        [DataMember(Name = "timeText")]
        public string TimeText { get; set; }

        [DataMember(Name = "start")]
        public string EventStart { get; set; }

        [DataMember(Name = "end")]
        public string EventEnd { get; set; }
    }
}
