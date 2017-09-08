using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// SubscribePO=订阅 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Subscribe"),
        TableSettings(TableName = "Subscribe", PrimaryKey = "Id", SortField = "Id")]
	public class SubscribePO : BaseEntity
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
		/// 项目
		/// </summary>
		[DisplayName("项目")]
		[UIHint("IntBox")]
		[XmlElement("ProjectId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ProjectId  { get; set; }

		/// <summary>
		/// 计划
		/// </summary>
		[DisplayName("计划")]
		[UIHint("IntBox")]
		[XmlElement("ScheduleId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int ScheduleId { get; set; }

		/// <summary>
		/// 风格
		/// </summary>
		[DisplayName("风格")]
		[UIHint("IntBox")]
		[XmlElement("DataStyleId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int DataStyleId  { get; set; }

		/// <summary>
		/// 已订阅
		/// </summary>
		[DisplayName("已订阅")]
		[UIHint("CheckBox")]
		[XmlElement("Subscribed"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Subscribed  { get; set; }

		/// <summary>
		/// 订阅时间
		/// </summary>
		[DisplayName("订阅时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("SubscribeTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime SubscribeTime  { get; set; }

		/// <summary>
		/// 取消订阅
		/// </summary>
		[DisplayName("取消订阅")]
		[UIHint("CheckBox")]
		[XmlElement("Unsubscribed"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Unsubscribed  { get; set; }

		/// <summary>
		/// 取消时间
		/// </summary>
		[DisplayName("取消时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("UnsubscribeTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime UnsubscribeTime  { get; set; }

		/// <summary>
		/// 已报名
		/// </summary>
		[DisplayName("已报名")]
		[UIHint("CheckBox")]
		[XmlElement("Entered"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Entered  { get; set; }

		/// <summary>
		/// 报名时间
		/// </summary>
		[DisplayName("报名时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("EnterTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime EnterTime  { get; set; }

		/// <summary>
		/// Cancelled
		/// </summary>
		[DisplayName("Cancelled")]
		[UIHint("CheckBox")]
		[XmlElement("Cancelled"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Cancelled  { get; set; }

		/// <summary>
		/// CancelTime
		/// </summary>
		[DisplayName("CancelTime")]
		[UIHint("DateTimeBox")]
		[XmlElement("CancelTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime CancelTime  { get; set; }

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
