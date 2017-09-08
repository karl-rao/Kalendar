using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kalendar.Zero.DB.Entity.Ext
{
	/// <summary>
    /// 结构性对象类型定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
	[Serializable, XmlRoot(ElementName = "StructSet")]
    public class StructSet<T> where T : Base.BaseEntity
    {
		/// <summary>
        /// Gets or sets the object list.
        /// </summary>
        /// <value>
        /// The object list.
        /// </value>
        public List<T> ObjectList { get; set; }
		/// <summary>
        /// Gets or sets the object count.
        /// </summary>
        /// <value>
        /// The object count.
        /// </value>
        public int ObjectCount { get; set; }
    }
}
