using System;
using System.Xml.Serialization;

namespace Kalendar.Zero.DB.Entity.Ext
{
	/// <summary>
    /// 查询空对象
    /// </summary>
     [Serializable, XmlRoot(ElementName = "PlusQueryPO")]
    public class PlusQueryPO : Base.BaseEntity
    {
    }
}