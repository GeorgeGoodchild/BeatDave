using System.Web.Mvc;
using AutoMapper;

namespace BeatDave.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<string, MvcHtmlString>().ConvertUsing<MvcHtmlStringConverter>();
            
            // TODO: Use reflection to add all profiles (or some IoC container or somethin')
            Mapper.AddProfile<LogBookProfile>();
            Mapper.AddProfile<UserProfile>();
        }
    }
}