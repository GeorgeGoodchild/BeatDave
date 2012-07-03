
namespace BeatDave.Web.Infrastructure
{
    using System;
    using System.Text.RegularExpressions;

    public class RavenIdResolver
    {        
        public static int ResolveToInt(string ravenId)
        {
            var match = Regex.Match(ravenId, @"\d+");
            var idStr = match.Value;

            int id = int.Parse(idStr);

            if (id <= 0)
                throw new InvalidOperationException("Id cannot be less than or equal to zero.");

            return id;
        }
    }
}