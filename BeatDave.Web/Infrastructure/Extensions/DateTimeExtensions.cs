
namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToReadableString(this DateTime instance)
        {
            var span = DateTime.UtcNow.Subtract(instance.ToUniversalTime());

            var days = span.Days;
            if (days > 0)
            {
                if (span.Hours > 23) days++;

                if (days > 31)
                    return instance.ToString("d MMM yy");

                if (days > 28)
                    return "About a month ago";

                if (days > 1)
                    return string.Format("{0:0} days ago", days);
                
                return "About a day ago";
            }

            if (span.Hours > 0)
            {
                if (span.Hours == 1)
                    return "About 1 hour ago";
                
                return string.Format("About {0:0} hours ago", span.Hours);
            }

            if (span.Minutes > 1)
            {
                return string.Format("{0:0} minutes ago", span.Minutes);
            }

            return "Just now";
        }
    }
}