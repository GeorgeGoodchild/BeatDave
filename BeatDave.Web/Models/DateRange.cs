
namespace BeatDave.Web.Models
{
    using System;

    public class DateRange
    {
        public static DateRange Forever = new DateRange { From = DateTime.MinValue, To = DateTime.MaxValue };

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        // override object.Equals
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