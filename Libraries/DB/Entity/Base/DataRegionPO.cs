using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// DataRegionPO=地区 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "DataRegion"),
        TableSettings(TableName = "DataRegion", PrimaryKey = "Id", SortField = "Id")]
	public class DataRegionPO : BaseEntity
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
		/// 地区
		/// </summary>
		[DisplayName("地区")]
		[StringLength(100, ErrorMessage = "作为地区字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("RegionName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string RegionName  { get; set; }

		/// <summary>
		/// 英文名
		/// </summary>
		[DisplayName("英文名")]
		[StringLength(100, ErrorMessage = "作为英文名字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("RegionNameEn"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string RegionNameEn  { get; set; }

		/// <summary>
		/// 二字编码
		/// </summary>
		[DisplayName("二字编码")]
		[StringLength(2, ErrorMessage = "作为二字编码字符串长度不能超过2!")]
		[UIHint("SmallBox")]
		[XmlElement("ISO3166A2"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ISO3166A2  { get; set; }

		/// <summary>
		/// 三字编码
		/// </summary>
		[DisplayName("三字编码")]
		[StringLength(3, ErrorMessage = "作为三字编码字符串长度不能超过3!")]
		[UIHint("SmallBox")]
		[XmlElement("ISO3166A3"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ISO3166A3  { get; set; }

		/// <summary>
		/// 数字编码
		/// </summary>
		[DisplayName("数字编码")]
		[UIHint("IntBox")]
		[XmlElement("ISO3166Num"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ISO3166Num  { get; set; }

		/// <summary>
		/// 支持
		/// </summary>
		[DisplayName("支持")]
		[UIHint("CheckBox")]
		[XmlElement("Applied"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Applied  { get; set; }

		/// <summary>
		/// 上级地区
		/// </summary>
		[DisplayName("上级地区")]
		[UIHint("IntBox")]
		[XmlElement("ParentId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ParentId  { get; set; }

		/// <summary>
		/// 显示顺序
		/// </summary>
		[DisplayName("显示顺序")]
		[UIHint("IntBox")]
		[XmlElement("DisplayRank"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DisplayRank  { get; set; }

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
