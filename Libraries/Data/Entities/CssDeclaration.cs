using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Kalendar.Zero.Data.Entities
{
    public class CssDeclaration
    {
        /// <summary>
        /// 背景颜色
        /// </summary>
        [DisplayName("背景颜色")]
        [UIHint("ColorBox")]
        public string BackgroundColor { get; set; } = "#FFF";

        public string FrontColor { get; set; } = "#000";

        public string BorderColor { get; set; } = "#333";

        public int BorderRadius { get; set; } = 3;

        public int BorderWidth { get; set; }= 1;

        public int Padding { get; set; } = 5;
    }
}
