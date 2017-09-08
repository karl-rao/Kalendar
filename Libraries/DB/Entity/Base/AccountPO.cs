using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// AccountPO=用户 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Account"),
        TableSettings(TableName = "Account", PrimaryKey = "Id", SortField = "Id")]
	public class AccountPO : BaseEntity
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
		/// 时区
		/// </summary>
		[DisplayName("时区")]
		[UIHint("IntBox")]
		[XmlElement("TimeZone"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int TimeZone  { get; set; }

		/// <summary>
		/// 地区
		/// </summary>
		[DisplayName("地区")]
		[UIHint("IntBox")]
		[XmlElement("DataRegionId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DataRegionId  { get; set; }

		/// <summary>
		/// 语种
		/// </summary>
		[DisplayName("语种")]
		[UIHint("IntBox")]
		[XmlElement("DataLanguageId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DataLanguageId  { get; set; }

		/// <summary>
		/// 昵称
		/// </summary>
		[DisplayName("昵称")]
		[StringLength(100, ErrorMessage = "作为昵称字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("NickName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string NickName  { get; set; }

		/// <summary>
		/// 头像
		/// </summary>
		[DisplayName("头像")]
		[StringLength(200, ErrorMessage = "作为头像字符串长度不能超过200!")]
		[UIHint("MultiBox")]
		[XmlElement("Portrait"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Portrait  { get; set; }

        /// <summary>
        /// 会员标识
        /// </summary>
        [DisplayName("会员标识")]
        [StringLength(50, ErrorMessage = "作为会员标识字符串长度不能超过50!")]
        [UIHint("MediumBox")]
        [XmlElement("PassportIdentity"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string PassportIdentity { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        [DisplayName("会员卡号")]
		[StringLength(100, ErrorMessage = "作为会员卡号字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("PassportCode"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string PassportCode  { get; set; }

		/// <summary>
		/// 通行证
		/// </summary>
		[DisplayName("通行证")]
		[StringLength(20, ErrorMessage = "作为通行证字符串长度不能超过20!")]
		[UIHint("SmallBox")]
		[XmlElement("PassportKey"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string PassportKey  { get; set; }

		/// <summary>
		/// 令牌
		/// </summary>
		[DisplayName("令牌")]
		[StringLength(32, ErrorMessage = "作为令牌字符串长度不能超过32!")]
		[UIHint("MediumBox")]
		[XmlElement("AccessToken"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string AccessToken  { get; set; }

		/// <summary>
		/// 邮件
		/// </summary>
		[DisplayName("邮件")]
		[StringLength(100, ErrorMessage = "作为邮件字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("Email"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Email  { get; set; }

		/// <summary>
		/// 是否邮件验证
		/// </summary>
		[DisplayName("是否邮件验证")]
		[UIHint("CheckBox")]
		[XmlElement("IsEmailVerified"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool IsEmailVerified  { get; set; }

		/// <summary>
		/// 邮件验证时间
		/// </summary>
		[DisplayName("邮件验证时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("EmailVerifyTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime EmailVerifyTime  { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		[DisplayName("手机")]
		[StringLength(20, ErrorMessage = "作为手机字符串长度不能超过20!")]
		[UIHint("SmallBox")]
		[XmlElement("Mobile"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Mobile  { get; set; }

		/// <summary>
		/// 是否手机验证
		/// </summary>
		[DisplayName("是否手机验证")]
		[UIHint("CheckBox")]
		[XmlElement("IsMobileVerified"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool IsMobileVerified  { get; set; }

		/// <summary>
		/// 手机验证时间
		/// </summary>
		[DisplayName("手机验证时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("MobileVerifyTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime MobileVerifyTime  { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		[DisplayName("密码")]
		[StringLength(32, ErrorMessage = "作为密码字符串长度不能超过32!")]
		[UIHint("MediumBox")]
		[XmlElement("LoginPassword"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string LoginPassword  { get; set; }

		/// <summary>
		/// 随即加密盐值
		/// </summary>
		[DisplayName("随即加密盐值")]
		[StringLength(10, ErrorMessage = "作为随即加密盐值字符串长度不能超过10!")]
		[UIHint("SmallBox")]
		[XmlElement("Salt"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Salt  { get; set; }

		/// <summary>
		/// 营销渠道主键
		/// </summary>
		[DisplayName("营销渠道主键")]
		[UIHint("IntBox")]
		[XmlElement("MarketingId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int MarketingId  { get; set; }

		/// <summary>
		/// 注册IP
		/// </summary>
		[DisplayName("注册IP")]
		[StringLength(15, ErrorMessage = "作为注册IP字符串长度不能超过15!")]
		[UIHint("SmallBox")]
		[XmlElement("RegisterIp"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string RegisterIp  { get; set; }

		/// <summary>
		/// 验证码
		/// </summary>
		[DisplayName("验证码")]
		[StringLength(10, ErrorMessage = "作为验证码字符串长度不能超过10!")]
		[UIHint("SmallBox")]
		[XmlElement("VerifyCode"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string VerifyCode  { get; set; }

		/// <summary>
		/// 验证码发送次数
		/// </summary>
		[DisplayName("验证码发送次数")]
		[UIHint("IntBox")]
		[XmlElement("VerifySendCount"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int VerifySendCount  { get; set; }

		/// <summary>
		/// 余额
		/// </summary>
		[DisplayName("余额")]
		[UIHint("MoneyBox")]
		[XmlElement("Balance"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal Balance  { get; set; }

		/// <summary>
		/// 信用额度
		/// </summary>
		[DisplayName("信用额度")]
		[UIHint("MoneyBox")]
		[XmlElement("Credit"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal Credit  { get; set; }

		/// <summary>
		/// 最后登陆时间
		/// </summary>
		[DisplayName("最后登陆时间")]
		[UIHint("DateTimeBox")]
		[Required(ErrorMessage ="请输入最后登陆时间!")]
		[XmlElement("LastSignin"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime LastSignin  { get; set; }

		/// <summary>
		/// 允许创建项目
		/// </summary>
		[DisplayName("允许创建项目")]
		[UIHint("CheckBox")]
		[XmlElement("CanCreateProject"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool CanCreateProject  { get; set; }

		/// <summary>
		/// 管理员
		/// </summary>
		[DisplayName("管理员")]
		[UIHint("CheckBox")]
		[XmlElement("IsRoot"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool IsRoot  { get; set; }

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
