
namespace BeatDave.Domain
{
    using System;

    public class LogBookEntry    
    {
        public LogBook LogBook { get; set; }
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}