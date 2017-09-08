﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Kalendar.Zero.Data.Filters
{
    /// <summary>
    /// 用户关系列表默认筛选条件
    /// </summary>
    public class AccountRelationFilter
    {
        [DisplayName("关键字")]
        [UIHint("MediumBox")]
        public string Keyword { get; set; }
        
    }
}