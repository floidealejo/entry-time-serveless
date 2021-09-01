using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace entry_time_serveless.Functions.Entities
{
    public class ConsolidatedEntity : TableEntity
    {
        public int IdEmployed { get; set; }
        public DateTime Date { get; set; }
        public int WorkingMinutes { get; set; }
    }
}
