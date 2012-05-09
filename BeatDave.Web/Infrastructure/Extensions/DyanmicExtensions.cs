
namespace BeatDave.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public static class DyanmicExtensions
    {
        public static dynamic Squash<T>(this T item, string[] fields)
        {
            dynamic d = new ExpandoObject();
            var dic = d as IDictionary<string, object>;
            
            foreach (var field in fields.Select(x => x.Trim()).Distinct())
            {
                var property = typeof(T).GetProperty(field, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (property != null)
                {
                    dic[field] = property.GetValue(item, null);
                }
            }

            return d;
        }
    }
}