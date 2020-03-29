using System;
using System.Collections.Generic;

namespace timeSheetBackEnd.Models
{
    public partial class Luokkahuone
    {
        public Luokkahuone()
        {
            TuntiRaportti = new HashSet<TuntiRaportti>();
        }

        public int LuokkahuoneId { get; set; }
        public string LuokkahuoneenNimi { get; set; }

        public virtual ICollection<TuntiRaportti> TuntiRaportti { get; set; }
    }
}
