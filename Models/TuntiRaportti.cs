using System;
using System.Collections.Generic;

namespace timeSheetBackEnd.Models
{
    public partial class TuntiRaportti
    {
        public int Idleimaus { get; set; }
        public DateTime? Sisaan { get; set; }
        public DateTime? Ulos { get; set; }
        public string OppilasId { get; set; }
        public int? LuokkahuoneId { get; set; }

        public virtual Luokkahuone Luokkahuone { get; set; }
        public virtual AspNetUsers Oppilas { get; set; }
    }
}
