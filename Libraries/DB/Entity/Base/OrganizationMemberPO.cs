using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// OrganizationMemberPO=组织成员 实体定义类
    /// </summary>
    [Serializable, 
        XmlRoot(ElementName = "OrganizationMember"),
        TableSettings(TableName = "OrganizationMember", PrimaryKey = "Id", SortField = "Id")]
	public class OrganizationMemberPO : BaseEntity
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
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [UIHint("IntBox")]
        [XmlElement("OrganizationDepartmentId"),
            FieldSettings(
            Generator = "assigned",
            IsIdentity = false,
            IsNullable = true,
            IsPrimaryKey = false,
            InsertRequired = true,
            DeleteRequired = false,
            UpdateRequired = true)]
        public virtual int OrganizationDepartmentId { get; set; }

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
		/// 创建者
		/// </summary>
		[DisplayName("创建者")]
		[UIHint("CheckBox")]
		[XmlElement("IsCreator"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool IsCreator  { get; set; }

		/// <summary>
		/// 上级
		/// </summary>
		[DisplayName("上级")]
		[UIHint("IntBox")]
		[XmlElement("Superior"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int Superior  { get; set; }

		/// <summary>
		/// 职务
		/// </summary>
		[DisplayName("职务")]
		[StringLength(100, ErrorMessage = "作为职务字符串长度不能超过100!")]
		[UIHint("LargeBox")]
		[XmlElement("Title"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string Title  { get; set; }

		/// <summary>
		/// 权限
		/// </summary>
		[DisplayName("权限")]
		[UIHint("IntBox")]
		[XmlElement("PermitLevel"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual int PermitLevel  { get; set; }

		/// <summary>
		/// 请求
		/// </summary>
		[DisplayName("请求")]
		[StringLength(280, ErrorMessage = "作为请求字符串长度不能超过280!")]
		[UIHint("MultiBox")]
		[XmlElement("RequestNote"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual string RequestNote  { get; set; }

		/// <summary>
		/// 请求时间
		/// </summary>
		[DisplayName("请求时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("RequestTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime RequestTime  { get; set; }

		/// <summary>
		/// 已接受
		/// </summary>
		[DisplayName("已接受")]
		[UIHint("CheckBox")]
		[XmlElement("Accepted"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual bool Accepted  { get; set; }

		/// <summary>
		/// 接受时间
		/// </summary>
		[DisplayName("接受时间")]
		[UIHint("DateTimeBox")]
		[XmlElement("AcceptTime"),
			FieldSettings(
			Generator = "assigned",
			IsIdentity = false,
			IsNullable = true,
			IsPrimaryKey = false,
			InsertRequired = true,
			DeleteRequired = false, 
			UpdateRequired = true)]
		public virtual System.DateTime AcceptTime  { get; set; }

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
