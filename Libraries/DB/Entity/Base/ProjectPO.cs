using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// ProjectPO=项目 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "Project"),
        TableSettings(TableName = "Project", PrimaryKey = "Id", SortField = "Id")]
	public class ProjectPO : BaseEntity
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
        /// 项目标识
        /// </summary>
        [DisplayName("项目标识")]
        [StringLength(100, ErrorMessage = "作为项目标识字符串长度不能超过30!")]
        [UIHint("MediumBox")]
        [XmlElement("ProjectCode"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [DisplayName("项目名称")]
		[StringLength(100, ErrorMessage = "作为项目名称字符串长度不能超过100!")]
		[UIHint("MediumBox")]
		[XmlElement("ProjectName"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ProjectName  { get; set; }

		/// <summary>
		/// 项目简介
		/// </summary>
		[DisplayName("项目简介")]
		[StringLength(280, ErrorMessage = "作为项目简介字符串长度不能超过280!")]
		[UIHint("MultiBox")]
		[XmlElement("ProjectIntroduction"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ProjectIntroduction  { get; set; }

		/// <summary>
		/// 项目图标
		/// </summary>
		[DisplayName("项目图标")]
		[StringLength(50, ErrorMessage = "作为项目图标字符串长度不能超过50!")]
		[UIHint("PictureBox")]
		[XmlElement("ProjectIcon"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string ProjectIcon  { get; set; }

		/// <summary>
		/// 上级项目
		/// </summary>
		[DisplayName("上级项目")]
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
		/// 样式
		/// </summary>
		[DisplayName("样式")]
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
		/// 项目费用
		/// </summary>
		[DisplayName("项目费用")]
		[UIHint("MoneyBox")]
		[XmlElement("ProjectExpense"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal ProjectExpense  { get; set; }

		/// <summary>
		/// 付款人
		/// </summary>
		[DisplayName("付款人")]
		[UIHint("IntBox")]
		[XmlElement("Drawee"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int Drawee  { get; set; }

		/// <summary>
		/// 均摊费用
		/// </summary>
		[DisplayName("均摊费用")]
		[UIHint("MoneyBox")]
		[XmlElement("ApportionExpense"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual decimal ApportionExpense  { get; set; }

		/// <summary>
		/// 截止时间
		/// </summary>
		[DisplayName("截止时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("EnterDeadline"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime EnterDeadline  { get; set; }

		/// <summary>
		/// 标签
		/// </summary>
		[DisplayName("标签")]
		[StringLength(200, ErrorMessage = "作为标签字符串长度不能超过200!")]
		[UIHint("LargeBox")]
		[XmlElement("Tags"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Tags  { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		[DisplayName("创建者")]
		[UIHint("IntBox")]
		[XmlElement("CreatorAccountId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int CreatorAccountId  { get; set; }

		/// <summary>
		/// 组织
		/// </summary>
		[DisplayName("组织")]
		[UIHint("IntBox")]
		[XmlElement("OrganizationId"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int OrganizationId  { get; set; }

		/// <summary>
		/// 订阅数
		/// </summary>
		[DisplayName("订阅数")]
		[UIHint("IntBox")]
		[XmlElement("SubscribedCount"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int SubscribedCount  { get; set; }

        /// <summary>
        /// 公开
        /// </summary>
        [DisplayName("公开")]
        [UIHint("CheckBox")]
        [XmlElement("IsPublic"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual bool IsPublic { get; set; }

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
