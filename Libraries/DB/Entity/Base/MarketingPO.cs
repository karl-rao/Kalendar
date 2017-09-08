using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// MarketingPO=推广渠道 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Marketing"),
        TableSettings(TableName = "Marketing", PrimaryKey = "Id", SortField = "Id")]
	public class MarketingPO : BaseEntity
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
		/// 推广营销标识
		/// </summary>
		[DisplayName("推广营销标识")]
		[StringLength(20, ErrorMessage = "作为推广营销标识字符串长度不能超过20!")]
		[UIHint("SmallBox")]
		[Required(ErrorMessage ="请输入推广营销标识!")]
		[XmlElement("MarketingCode"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string MarketingCode  { get; set; }

		/// <summary>
		/// 推广营销名称
		/// </summary>
		[DisplayName("推广营销名称")]
		[StringLength(100, ErrorMessage = "作为推广营销名称字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[Required(ErrorMessage ="请输入推广营销名称!")]
		[XmlElement("MarketingName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string MarketingName  { get; set; }

		/// <summary>
		/// 报表查看账号
		/// </summary>
		[DisplayName("报表查看账号")]
		[StringLength(32, ErrorMessage = "作为报表查看账号字符串长度不能超过32!")]
		[UIHint("MediumBox")]
		[XmlElement("ReportAccount"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ReportAccount  { get; set; }

		/// <summary>
		/// 报表查看密码
		/// </summary>
		[DisplayName("报表查看密码")]
		[StringLength(32, ErrorMessage = "作为报表查看密码字符串长度不能超过32!")]
		[UIHint("MediumBox")]
		[XmlElement("ReportPassword"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ReportPassword  { get; set; }

		/// <summary>
		/// 通知邮箱
		/// </summary>
		[DisplayName("通知邮箱")]
		[StringLength(250, ErrorMessage = "作为通知邮箱字符串长度不能超过250!")]
		[UIHint("MultiBox")]
		[XmlElement("NotifyEmail"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string NotifyEmail  { get; set; }

		/// <summary>
		/// 用户
		/// </summary>
		[DisplayName("用户")]
		[UIHint("IntBox")]
		[XmlElement("CustomerId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int CustomerId  { get; set; }

		/// <summary>
		/// 过期天数(客户端记录Cookie周期)
		/// </summary>
		[DisplayName("过期天数(客户端记录Cookie周期)")]
		[UIHint("IntBox")]
		[Required(ErrorMessage ="请输入过期天数(客户端记录Cookie周期)!")]
		[XmlElement("ExpireDays"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ExpireDays  { get; set; }

		/// <summary>
		/// 按点击付费
		/// </summary>
		[DisplayName("按点击付费")]
		[UIHint("MoneyBox")]
		[Required(ErrorMessage ="请输入按点击付费!")]
		[XmlElement("CPC"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal CPC  { get; set; }

		/// <summary>
		/// 按行为付费(注册)
		/// </summary>
		[DisplayName("按行为付费(注册)")]
		[UIHint("MoneyBox")]
		[Required(ErrorMessage ="请输入按行为付费(注册)!")]
		[XmlElement("CPA"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal CPA  { get; set; }

		/// <summary>
		/// 按展示付费
		/// </summary>
		[DisplayName("按展示付费")]
		[UIHint("MoneyBox")]
		[Required(ErrorMessage ="请输入按展示付费!")]
		[XmlElement("CPM"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal CPM  { get; set; }

		/// <summary>
		/// 佣金，按销售付费
		/// </summary>
		[DisplayName("佣金，按销售付费")]
		[UIHint("DecimalBox")]
		[Required(ErrorMessage ="请输入佣金，按销售付费!")]
		[XmlElement("CPSCommission"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal CPSCommission  { get; set; }

		/// <summary>
		/// 按浏览收费
		/// </summary>
		[DisplayName("按浏览收费")]
		[UIHint("MoneyBox")]
		[Required(ErrorMessage ="请输入按浏览收费!")]
		[XmlElement("CPV"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = false,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal CPV  { get; set; }

		/// <summary>
		/// 最后结算时间
		/// </summary>
		[DisplayName("最后结算时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("LastSettleTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime LastSettleTime  { get; set; }

		/// <summary>
		/// 总收入
		/// </summary>
		[DisplayName("总收入")]
		[UIHint("MoneyBox")]
		[XmlElement("Amount"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal Amount  { get; set; }

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
		/// 有效
		/// </summary>
		[DisplayName("有效")]
		[UIHint("CheckBox")]
		[Required(ErrorMessage ="请输入有效!")]
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
		/// 最后更新
		/// </summary>
		[DisplayName("最后更新")]
		[UIHint("DateTimeBox")]
		[Required(ErrorMessage ="请输入最后更新!")]
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
		/// 创建时间
		/// </summary>
		[DisplayName("创建时间")]
		[UIHint("DateTimeBox")]
		[Required(ErrorMessage ="请输入创建时间!")]
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
