using System.IO;

namespace Kalendar.Zero.ApiTerminal.CalDav {
	public interface ISerializeToICAL {
		void Deserialize(TextReader rdr, Serializer serializer);
		void Serialize(TextWriter wrtr);
	}
}
