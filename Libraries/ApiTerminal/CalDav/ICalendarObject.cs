using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.CalDav {
	public interface ICalendarObject : ISerializeToICAL {
		string UID { get; set; }
		int? Sequence { get; set; }
		DateTime? LastModified { get; set; }
		Calendar Calendar { get; set; }
	}
}
