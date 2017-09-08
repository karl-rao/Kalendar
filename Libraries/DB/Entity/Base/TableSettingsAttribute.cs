using System;

namespace Kalendar.Zero.DB.Entity.Base
{
    public class TableSettingsAttribute
        :Attribute
    {
        public string TableName { get; set; }

        public string SortField { get; set; }

        public string PrimaryKey { get; set; }
    }
}
