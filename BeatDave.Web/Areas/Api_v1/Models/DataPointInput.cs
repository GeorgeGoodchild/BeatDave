using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class DataPointInput
    {
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}