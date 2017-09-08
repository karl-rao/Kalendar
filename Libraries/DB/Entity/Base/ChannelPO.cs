using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// ChannelPO=渠道 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Channel"),
        TableSettings(TableName = "Channel", PrimaryKey = "Id", SortField = "Id")]
	public class ChannelPO : BaseEntity
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
		/// 渠道名
		/// </summary>
		[DisplayName("渠道名")]
		[StringLength(100, ErrorMessage = "作为渠道名字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("ChannelName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ChannelName  { get; set; }

		/// <summary>
		/// 渠道标识
		/// </summary>
		[DisplayName("渠道标识")]
		[UIHint("IntBox")]
		[XmlElement("ChannelSymbol"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ChannelSymbol  { get; set; }

		/// <summary>
		/// 渠道链接
		/// </summary>
		[DisplayName("渠道链接")]
		[StringLength(50, ErrorMessage = "作为渠道链接字符串长度不能超过50!")]
		[UIHint("MediumBox")]
		[XmlElement("ChannelUrl"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ChannelUrl  { get; set; }

		/// <summary>
		/// 应用ID
		/// </summary>
		[DisplayName("应用ID")]
		[StringLength(50, ErrorMessage = "作为应用ID字符串长度不能超过50!")]
		[UIHint("MediumBox")]
		[XmlElement("AppId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string AppId  { get; set; }

		/// <summary>
		/// 应用Secret
		/// </summary>
		[DisplayName("应用Secret")]
		[StringLength(100, ErrorMessage = "作为应用Secret字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("AppSecret"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string AppSecret  { get; set; }

		/// <summary>
		/// Code回叫地址
		/// </summary>
		[DisplayName("Code回叫地址")]
		[StringLength(100, ErrorMessage = "作为Code回叫地址字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("CodeCallback"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string CodeCallback  { get; set; }

		/// <summary>
		/// Token回叫地址
		/// </summary>
		[DisplayName("Token回叫地址")]
		[StringLength(100, ErrorMessage = "作为Token回叫地址字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("TokenCallback"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string TokenCallback  { get; set; }

		/// <summary>
		/// 参数
		/// </summary>
		[DisplayName("参数")]
		[StringLength(100, ErrorMessage = "作为参数字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("Parameters"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Parameters  { get; set; }

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
