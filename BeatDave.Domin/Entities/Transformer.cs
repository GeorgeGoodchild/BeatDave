
namespace BeatDave.Domain
{
    using System;

    public class Transformer
    {
        public DateRange IncludeEntriesInRange { get; set; }
        public TimeSpan FirstEntryOffset { get; set; }
        public double UnitsMultiplier { get; set; }

        public LogBookEntry Transform(LogBookEntry entry)
        {
            return new LogBookEntry
            {
                LogBook = entry.LogBook,
                Id = entry.Id,
                OccurredOn = entry.OccurredOn.Add(this.FirstEntryOffset),
                Value = entry.Value * UnitsMultiplier
            };
        }
    }
}