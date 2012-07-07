using System;
using BeatDave.Web.Areas.Api_v1.Models;

namespace BeatDave.Web.Infrastructure
{
    public class DateTimeViewResolver
    {
        public static DateTimeView Resolve(DateTime value)
        {
            return new DateTimeView { Value = value.ToLocalTime(), ReadableValue = value.ToReadableString() };
        }
    }
}
