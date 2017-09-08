using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// AccountContactsPO=用户联系人 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "AccountContacts"),
        TableSettings(TableName = "AccountContacts", PrimaryKey = "Id", SortField = "Id")]
	public class AccountContactsPO : BaseEntity
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
		public virtual int AccountId  { get; set; }

		/// <summary>
		/// 渠道
		/// </summary>
		[DisplayName("渠道")]
		[UIHint("IntBox")]
		[XmlElement("ChannelId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ChannelId  { get; set; }

        /// <summary>
        /// 渠道标识
        /// </summary>
        [DisplayName("渠道标识")]
        [StringLength(500, ErrorMessage = "作为渠道标识字符串长度不能超过500!")]
        [UIHint("MediumBox")]
        [XmlElement("ChannelIdentity"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string ChannelIdentity { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        [DisplayName("显示名")]
		[StringLength(100, ErrorMessage = "作为显示名字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("DisplayName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string DisplayName  { get; set; }

		/// <summary>
		/// 明细
		/// </summary>
		[DisplayName("明细")]
		[UIHint("RichBox")]
		[XmlElement("Detail"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Detail  { get; set; }

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
