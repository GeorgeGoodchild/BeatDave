
namespace BeatDave.Web.Models
{
    using System;

    public class Entry    
    {
        public LogBook LogBook { get; set; }
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}