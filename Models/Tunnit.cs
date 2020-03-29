using System;
using System.Collections.Generic;

namespace timeSheetBackEnd.Models
{
    public partial class Tunnit
    {
        public int TunnitId { get; set; }
        public string LuokkahuoneId { get; set; }
        public DateTime? Sisaan { get; set; }
    }
}
