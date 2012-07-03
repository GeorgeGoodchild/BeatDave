using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;

namespace BeatDave.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<string, MvcHtmlString>().ConvertUsing<MvcHtmlStringConverter>();

            var profiles = from t in Assembly.GetAssembly(typeof(AutoMapperConfiguration)).GetTypes()
                           where t.IsSubclassOf(typeof(Profile))
                           select t;

            foreach (var p in profiles)
            {                
                typeof(Mapper).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod)
                              .First(x=> x.Name == "AddProfile" && x.GetGenericArguments().Count() > 0)
                              .MakeGenericMethod(p)
                              .Invoke(null, null);
            }
        }
    }
}