using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace entry_time_serveless.Functions.Entities
{
    public class EntryTimeEntity : TableEntity
    {
        public int IdEmployed { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}
