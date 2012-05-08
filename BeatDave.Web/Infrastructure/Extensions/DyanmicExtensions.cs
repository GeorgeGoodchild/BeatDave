
namespace BeatDave.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Dynamic;

    public static class DyanmicExtensions
    {
        public static dynamic Squash<T>(this T item, string[] fields)
        {
            dynamic d = new ExpandoObject();
            var dic = d as IDictionary<string, object>;

            foreach (var field in fields)
            {
                var trimmedField = field.Trim();
                dic[trimmedField] = typeof(T).GetProperty(trimmedField).GetValue(item, null);
            }

            return d;
        }
    }
}