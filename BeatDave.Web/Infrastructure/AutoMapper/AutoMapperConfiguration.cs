using System.Web.Mvc;
using AutoMapper;

namespace BeatDave.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<string, MvcHtmlString>().ConvertUsing<MvcHtmlStringConverter>();

            Mapper.AddProfile<DataSetProfile>();
        }
    }
}