using System;
using System.Text.RegularExpressions;

namespace BeatDave.Web.Infrastructure
{    
    public class RavenIdResolver
    {        
        public static int Resolve(string ravenId)
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