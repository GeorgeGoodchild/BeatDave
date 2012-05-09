
namespace BeatDave.Web.Models
{
    using System;

    public class Transformer
    {
        public DateRange IncludeRecordsInRange { get; set; }
        public TimeSpan FirstRecordOffset { get; set; }
        public double UnitsMultiplier { get; set; }

        public Record Transform(Record r)
        {
            return new Record
            {
                LogBook = r.LogBook,
                Id = r.Id,
                OccurredOn = r.OccurredOn.Add(this.FirstRecordOffset),
                Value = r.Value * UnitsMultiplier
            };
        }
    }
}