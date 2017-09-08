using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// AccountAvatarsPO=用户分身 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "AccountAvatars"),
        TableSettings(TableName = "AccountAvatars",PrimaryKey = "Id",SortField = "Id")]
	public class AccountAvatarsPO : BaseEntity
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
		[StringLength(50, ErrorMessage = "作为渠道标识字符串长度不能超过50!")]
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
		public virtual string ChannelIdentity  { get; set; }

        /// <summary>
        /// OAuth2Code
        /// </summary>
        [DisplayName("OAuth2Code")]
		[StringLength(50, ErrorMessage = "作为OAuth2Code字符串长度不能超过50!")]
		[UIHint("MediumBox")]
		[XmlElement("Code"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Code  { get; set; }

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
        public virtual string DisplayName { get; set; }

		/// <summary>
		/// OAuth2Token
		/// </summary>
		[DisplayName("OAuth2Token")]
		[StringLength(50, ErrorMessage = "作为OAuth2Token字符串长度不能超过1000!")]
		[UIHint("MediumBox")]
		[XmlElement("Token"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Token  { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [DisplayName("RefreshToken")]
        [StringLength(50, ErrorMessage = "作为RefreshToken字符串长度不能超过1000!")]
        [UIHint("MediumBox")]
        [XmlElement("RefreshToken"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string RefreshToken { get; set; }

        /// <summary>
        /// 令牌生成时间
        /// </summary>
        [DisplayName("令牌生成时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("TokenGenerated"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime TokenGenerated  { get; set; }

        /// <summary>
        /// 令牌过期时间
        /// </summary>
        [DisplayName("令牌过期时间")]
        [UIHint("DateTimeBox")]
        [XmlElement("TokenExpires"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual System.DateTime TokenExpires { get; set; }

        /// <summary>
        /// 上次同步时间
        /// </summary>
        [DisplayName("上次同步时间")]
        [UIHint("DateTimeBox")]
        [XmlElement("SynchroTime"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual System.DateTime SynchroTime { get; set; }

        /// <summary>
        /// 同步周期(秒)
        /// </summary>
        [DisplayName("同步周期")]
        [UIHint("IntBox")]
        [XmlElement("SynchroDuration"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual int SynchroDuration { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        [DisplayName("配置")]
		[UIHint("RichBox")]
		[XmlElement("Profile"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Profile  { get; set; }

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
