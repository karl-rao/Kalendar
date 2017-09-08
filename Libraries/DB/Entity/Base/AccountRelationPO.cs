using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// AccountRelationPO=用户关系 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "AccountRelation"),
        TableSettings(TableName = "AccountRelation", PrimaryKey = "Id", SortField = "Id")]
	public class AccountRelationPO : BaseEntity
    { 
		/// <summary>
		/// 主键
		/// </summary>
		[DisplayName("主键")]
		[UIHint("IntBox")]
		[Required(ErrorMessage ="请输入主键!")]
		[XmlElement("Id"),
			FieldSettings(
			Generator = "identity",
			IsIdentity = true,
			IsNullable = false,
			IsPrimaryKey = true,
			InsertRequired = false,
			DeleteRequired = true, 
			UpdateRequired = false)]
		public virtual int Id  { get; set; }

		/// <summary>
		/// 主动用户
		/// </summary>
		[DisplayName("主动用户")]
		[UIHint("IntBox")]
		[XmlElement("ActiveAccountId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ActiveAccountId  { get; set; }

		/// <summary>
		/// 授权
		/// </summary>
		[DisplayName("授权")]
		[UIHint("IntBox")]
		[XmlElement("ActivePermitLevel"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ActivePermitLevel  { get; set; }

		/// <summary>
		/// 被动用户
		/// </summary>
		[DisplayName("被动用户")]
		[UIHint("IntBox")]
		[XmlElement("PassiveAccountId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int PassiveAccountId  { get; set; }

		/// <summary>
		/// 授权
		/// </summary>
		[DisplayName("授权")]
		[UIHint("IntBox")]
		[XmlElement("PassiveLevelId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int PassiveLevelId  { get; set; }

		/// <summary>
		/// 请求
		/// </summary>
		[DisplayName("请求")]
		[StringLength(280, ErrorMessage = "作为请求字符串长度不能超过280!")]
		[UIHint("MultiBox")]
		[XmlElement("RequestNote"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string RequestNote  { get; set; }

		/// <summary>
		/// 请求时间
		/// </summary>
		[DisplayName("请求时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("RequestTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime RequestTime  { get; set; }

		/// <summary>
		/// 已接受
		/// </summary>
		[DisplayName("已接受")]
		[UIHint("CheckBox")]
		[XmlElement("Accepted"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Accepted  { get; set; }

		/// <summary>
		/// 接受时间
		/// </summary>
		[DisplayName("接受时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("AcceptTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime AcceptTime  { get; set; }

		/// <summary>
		/// 有效
		/// </summary>
		[DisplayName("有效")]
		[UIHint("CheckBox")]
		[XmlElement("Valid"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Valid  { get; set; }

		/// <summary>
		/// 最后更新
		/// </summary>
		[DisplayName("最后更新")]
		[UIHint("DateTimeBox")]
		[XmlElement("UpdateTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime UpdateTime  { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[DisplayName("创建时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("CreateTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime CreateTime  { get; set; }


    }
}
