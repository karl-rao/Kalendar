using System;

namespace Kalendar.Zero.DB.Entity.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class FieldSettingsAttribute
        :Attribute
    {
        private string _generator = "assigned";

        /// <summary>
        /// Gets or sets the generator.
        /// </summary>
        /// <value>
        /// The generator.
        /// </value>
        public string Generator
        {
            get { return _generator; }
            set { _generator = value; }
        }

        private bool _insertRequired = true;
        /// <summary>
        /// Gets or sets a value indicating whether [insert required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [insert required]; otherwise, <c>false</c>.
        /// </value>
        public bool InsertRequired
        {
            get { return _insertRequired; }
            set { _insertRequired = value; }
        }

        private bool _updateRequired = true;
        /// <summary>
        /// Gets or sets a value indicating whether [update required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [update required]; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateRequired
        {
            get { return _updateRequired; }
            set { _updateRequired = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [delete required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete required]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is primary key; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is identity.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is identity; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity { get; set; }

        private bool _isNullable = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable {
            get { return _isNullable; }
            set { _isNullable = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSettingsAttribute"/> class.
        /// </summary>
        public FieldSettingsAttribute()
        {
            Generator = "assigned";
            InsertRequired = true;
            UpdateRequired = true;
            DeleteRequired = false;
            IsPrimaryKey = false;
            IsIdentity = false;
            IsNullable = false;
        }

        /// <summary>
        /// 初始化一个特性
        /// </summary>
        /// <param name="generator">数据库字段名</param>
        public FieldSettingsAttribute( string generator)
        {
            Generator =generator;
            InsertRequired = true;
            UpdateRequired = true;
            DeleteRequired = false;
            IsPrimaryKey = false;
            IsIdentity = false;
            IsNullable = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSettingsAttribute"/> class.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="insertRequired">if set to <c>true</c> [insert required].</param>
        /// <param name="updateRequired">if set to <c>true</c> [update required].</param>
        /// <param name="deleteRequired">if set to <c>true</c> [delete required].</param>
        /// <param name="isPrimaryKey">if set to <c>true</c> [is primary key].</param>
        /// <param name="isIdentity">if set to <c>true</c> [is identity].</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        public FieldSettingsAttribute(
            string generator,
            bool insertRequired, 
            bool updateRequired, 
            bool deleteRequired,
            bool isPrimaryKey,
            bool isIdentity,
            bool isNullable)
        {
            Generator = generator;
            InsertRequired = insertRequired;
            UpdateRequired = updateRequired;
            DeleteRequired = deleteRequired;
            IsPrimaryKey = isPrimaryKey;
            IsIdentity = isIdentity;
            IsNullable = isNullable;
        }
    }
}