using AutoMapper;
using System.Web.Mvc;

namespace BeatDave.Web.Infrastructure
{
    public class MvcHtmlStringConverter : TypeConverter<string, MvcHtmlString>
    {
        protected override MvcHtmlString ConvertCore(string source)
        {
            return MvcHtmlString.Create(source);
        }
    }
}
