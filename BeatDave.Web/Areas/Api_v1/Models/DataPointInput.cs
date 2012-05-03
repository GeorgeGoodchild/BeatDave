using System;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class DataPointInput
    {
        [Obsolete] public int DataSetId { get; set; }
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}