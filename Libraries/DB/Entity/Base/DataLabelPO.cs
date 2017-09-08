using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// DataLabelPO=网站标签表 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "DataLabel"),
        TableSettings(TableName = "DataLabel", PrimaryKey = "Id", SortField = "Id")]
	public class DataLabelPO : BaseEntity
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
		/// 标签编码
		/// </summary>
		[DisplayName("标签编码")]
		[StringLength(20, ErrorMessage = "作为标签编码字符串长度不能超过20!")]
		[UIHint("SmallBox")]
		[XmlElement("LabelCode"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string LabelCode  { get; set; }

		/// <summary>
		/// 标签名称
		/// </summary>
		[DisplayName("标签名称")]
		[StringLength(100, ErrorMessage = "作为标签名称字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("LabelName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string LabelName  { get; set; }

		/// <summary>
		/// 标签图标
		/// </summary>
		[DisplayName("标签图标")]
		[StringLength(300, ErrorMessage = "作为标签图标字符串长度不能超过300!")]
		[UIHint("PictureBox")]
		[XmlElement("LabelIcon"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string LabelIcon  { get; set; }

		/// <summary>
		/// 标签文本
		/// </summary>
		[DisplayName("标签文本")]
		[UIHint("RichBox")]
		[XmlElement("LabelContent"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string LabelContent  { get; set; }

		/// <summary>
		/// 数据类型
		/// </summary>
		[DisplayName("数据类型")]
		[UIHint("IntBox")]
		[Required(ErrorMessage ="请输入数据类型!")]
		[XmlElement("DataType"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DataType  { get; set; }

		/// <summary>
		/// 显示顺序
		/// </summary>
		[DisplayName("显示顺序")]
		[UIHint("IntBox")]
		[Required(ErrorMessage ="请输入显示顺序!")]
		[XmlElement("DisplayRank"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DisplayRank  { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		[DisplayName("是否有效")]
		[UIHint("CheckBox")]
		[Required(ErrorMessage ="请输入是否有效!")]
		[XmlElement("Valid"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Valid  { get; set; }

		/// <summary>
		/// 记录最后修改时间
		/// </summary>
		[DisplayName("记录最后修改时间")]
		[UIHint("DateTimeBox")]
		[Required(ErrorMessage ="请输入记录最后修改时间!")]
		[XmlElement("UpdateTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime UpdateTime  { get; set; }

		/// <summary>
		/// 记录创建时间
		/// </summary>
		[DisplayName("记录创建时间")]
		[UIHint("DateTimeBox")]
		[Required(ErrorMessage ="请输入记录创建时间!")]
		[XmlElement("CreateTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime CreateTime  { get; set; }


    }
}
