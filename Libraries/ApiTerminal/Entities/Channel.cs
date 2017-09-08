using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    public class Channel
    {
        public int Id { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string CodeCallback { get; set; }

        public string Parameters { get; set; }

    }
}
