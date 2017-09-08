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
    public class MsonlineRecurrencePattern
    {
        /// <summary>
        /// absoluteYearly
        /// absoluteMonthly
        /// weekly
        /// daily
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "interval")]
        public int Interval { get; set; }


        [DataMember(Name = "month")]
        public int Month { get; set; }

        [DataMember(Name = "dayOfMonth")]
        public int DayOfMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "daysOfWeek")]
        public List<string> DaysOfWeek { get; set; }

        [DataMember(Name = "firstDayOfWeek")]
        public string FirstDayOfWeek { get; set; }

        [DataMember(Name = "index")]
        public string Index { get; set; }
    }
}
