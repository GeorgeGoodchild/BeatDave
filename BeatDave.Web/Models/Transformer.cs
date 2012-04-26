
namespace BeatDave.Web.Models
{
    using System;

    public class Transformer
    {
        public DateRange IncludeDataPointsInRange { get; set; }
        public TimeSpan FirstDataPointOffset { get; set; }
        public double UnitsMultiplier { get; set; }

        public DataPoint Transform(DataPoint dp)
        {
            return new DataPoint
            {
                DataSet = dp.DataSet,
                Id = dp.Id,
                OccurredOn = dp.OccurredOn.Add(this.FirstDataPointOffset),
                Value = dp.Value * UnitsMultiplier
            };
        }
    }
}