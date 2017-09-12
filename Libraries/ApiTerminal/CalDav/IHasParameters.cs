using System.Collections.Specialized;

namespace Kalendar.Zero.ApiTerminal.CalDav {
	public interface IHasParameters {
		NameValueCollection GetParameters();
		void Deserialize(string value, NameValueCollection parameters);
	}
}
