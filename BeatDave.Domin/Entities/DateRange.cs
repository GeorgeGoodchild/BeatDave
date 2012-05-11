
namespace BeatDave.Domain
{
    using System;

    public class DateRange
    {
        public static DateRange Forever = new DateRange(DateTime.MinValue, DateTime.MaxValue);

        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public DateRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }

        // System.Object overrides
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var other = (DateRange)obj;

            return this.From.Equals(other.From) && this.To.Equals(other.To);
        }
    }
}