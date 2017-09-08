using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
		/// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            var stream = new MemoryStream();

            xmlSerializer.Serialize(stream, this);
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}