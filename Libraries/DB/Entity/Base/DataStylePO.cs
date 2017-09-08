using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// DataStylePO=风格 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "DataStyle"),
        TableSettings(TableName = "DataStyle", PrimaryKey = "Id", SortField = "Id")]
	public class DataStylePO : BaseEntity
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
		/// 样式名称
		/// </summary>
		[DisplayName("样式名称")]
		[StringLength(100, ErrorMessage = "作为样式名称字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("StyleName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string StyleName  { get; set; }

		/// <summary>
		/// 样式表
		/// </summary>
		[DisplayName("样式表")]
		[UIHint("MultiBox")]
		[XmlElement("StyleCss"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string StyleCss  { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [DisplayName("用户")]
        [UIHint("IntBox")]
        [XmlElement("AccountId"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual int AccountId { get; set; }

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
