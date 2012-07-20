using System.Linq;
using System.Reflection;
using AutoMapper;
using StructureMap;

namespace BeatDave.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {            
            var profiles = from t in Assembly.GetAssembly(typeof(AutoMapperConfiguration)).GetTypes()
                           where t.IsSubclassOf(typeof(Profile))
                           select ObjectFactory.GetInstance(t);


            foreach (var p in profiles.Cast<Profile>())
            {
                Mapper.AddProfile(p);
            }
        }
    }
}