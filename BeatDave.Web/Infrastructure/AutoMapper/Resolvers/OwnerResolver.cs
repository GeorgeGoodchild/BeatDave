
namespace BeatDave.Web.Infrastructure
{
    using System;
    using System.Security.Principal;
    using BeatDave.Domain;

    public static class OwnerNameResolver
    {        
        public static string Resolve(LogBook logBook, Func<IPrincipal> getUser)
        {
            if (logBook.Visibility == Visibility.PublicAnonymous && logBook.IsOwnedBy(getUser().Identity.Name) == false)
                return null;

            return logBook.OwnerId;
        }
    }
}