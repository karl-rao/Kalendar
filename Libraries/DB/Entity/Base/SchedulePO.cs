using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// SchedulePO=计划 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Schedule"),
        TableSettings(TableName = "Schedule", PrimaryKey = "Id", SortField = "Id")]
	public class SchedulePO : BaseEntity
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
        /// 排期标识
        /// </summary>
        [DisplayName("排期标识")]
        [StringLength(200, ErrorMessage = "作为排期标识字符串长度不能超过200!")]
        [UIHint("MultiBox")]
        [XmlElement("ScheduleIdentity"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string ScheduleIdentity { get; set; }

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
        /// 主题
        /// </summary>
        [DisplayName("主题")]
        [UIHint("LargeBox")]
        [XmlElement("ScheduleTitle"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string ScheduleTitle { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [UIHint("MultiBox")]
        [XmlElement("ScheduleLead"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string ScheduleLead { get; set; }

        /// <summary>
        /// 已计划
        /// </summary>
        [DisplayName("已计划")]
		[UIHint("IntBox")]
		[XmlElement("Planned"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int Planned  { get; set; }

		/// <summary>
		/// 预期时间
		/// </summary>
		[DisplayName("预期时间")]
		[UIHint("IntBox")]
		[XmlElement("EstimateSeconds"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int EstimateSeconds  { get; set; }

		/// <summary>
		/// 周期
		/// </summary>
		[DisplayName("周期")]
		[UIHint("IntBox")]
		[XmlElement("Cycle"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int Cycle  { get; set; }

		/// <summary>
		/// 开始日期
		/// </summary>
		[DisplayName("开始日期")]
		[UIHint("DateTimeBox")]
		[XmlElement("BeginDate"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime BeginDate  { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		[DisplayName("开始时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("BeginTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime BeginTime  { get; set; }

		/// <summary>
		/// 结束日期
		/// </summary>
		[DisplayName("结束日期")]
		[UIHint("DateTimeBox")]
		[XmlElement("EndDate"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime EndDate  { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		[DisplayName("结束时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("EndTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime EndTime  { get; set; }

		/// <summary>
		/// 开始日
		/// </summary>
		[DisplayName("开始日")]
		[UIHint("IntBox")]
		[XmlElement("BeginDay"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int BeginDay  { get; set; }

		/// <summary>
		/// 结束日
		/// </summary>
		[DisplayName("结束日")]
		[UIHint("IntBox")]
		[XmlElement("EndDay"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int EndDay  { get; set; }

		/// <summary>
		/// 有期限
		/// </summary>
		[DisplayName("有期限")]
		[UIHint("CheckBox")]
		[XmlElement("HadDeadline"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool HadDeadline  { get; set; }

		/// <summary>
		/// 截止时间
		/// </summary>
		[DisplayName("截止时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("Deadline"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime Deadline  { get; set; }

        /// <summary>
        /// 备注事项
        /// </summary>
        [DisplayName("备注")]
        [UIHint("RichBox")]
        [XmlElement("Note"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string Note { get; set; }

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
