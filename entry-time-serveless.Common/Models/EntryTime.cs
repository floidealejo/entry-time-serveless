using System;

namespace entry_time_serveless.Common.Models
{
    public class EntryTime
    {
        public int IdEmployed { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}
