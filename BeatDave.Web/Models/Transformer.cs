
namespace BeatDave.Web.Models
{
    using System;

    public class Transformer
    {
        public DateRange IncludeEntriesInRange { get; set; }
        public TimeSpan FirstEntryOffset { get; set; }
        public double UnitsMultiplier { get; set; }

        public Entry Transform(Entry r)
        {
            return new Entry
            {
                LogBook = r.LogBook,
                Id = r.Id,
                OccurredOn = r.OccurredOn.Add(this.FirstEntryOffset),
                Value = r.Value * UnitsMultiplier
            };
        }
    }
}