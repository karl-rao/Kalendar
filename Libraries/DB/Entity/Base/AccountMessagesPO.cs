using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// AccountMessagesPO=用户消息 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "AccountMessages"),
        TableSettings(TableName = "AccountMessages", PrimaryKey = "Id", SortField = "Id")]
	public class AccountMessagesPO : BaseEntity
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
        public virtual int ChannelId { get; set; }

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
        /// 收件方
        /// </summary>
        [DisplayName("收件方")]
		[UIHint("IntBox")]
		[XmlElement("ToAccountId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ToAccountId  { get; set; }

		/// <summary>
		/// 消息类型
		/// </summary>
		[DisplayName("消息类型")]
		[UIHint("IntBox")]
		[XmlElement("MessageType"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int MessageType  { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        [DisplayName("消息标题")]
        [UIHint("LargeBox")]
        [XmlElement("MessageSubject"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string MessageSubject { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        [DisplayName("消息正文")]
		[UIHint("RichBox")]
		[XmlElement("MessageContent"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string MessageContent  { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        [DisplayName("消息链接")]
        [UIHint("LargeBox")]
        [XmlElement("Weblink"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string Weblink { get; set; }

        /// <summary>
        /// 已读
        /// </summary>
        [DisplayName("已读")]
		[UIHint("CheckBox")]
		[XmlElement("Readed"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Readed  { get; set; }

		/// <summary>
		/// 读取时间
		/// </summary>
		[DisplayName("读取时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("ReadTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime ReadTime  { get; set; }

		/// <summary>
		/// 发送方
		/// </summary>
		[DisplayName("发送方")]
		[UIHint("IntBox")]
		[XmlElement("FromAccountId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int FromAccountId  { get; set; }

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
