using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace BeatDave.Web.Infrastructure
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {        
        // C'tor
        public BasicAuthorizeAttribute()
        { }


        // AuthorizeAttribute Overrides
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAuthenticatedUser(actionContext))
                return;

            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var challengeMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            
            throw new HttpResponseException(challengeMessage);            
        }


        // Private Members
        private bool IsAuthenticatedUser(HttpActionContext actionContext)
        {           
            if (actionContext.Request.Headers.Authorization == null) 
                return false;
            
            IPrincipal principal;
            if (TryGetPrincipal(actionContext.Request.Headers.Authorization, out principal))
            {
                ShortConversation.Data[AppConstants.UserKey] = principal;
                return true;
            }
            return false;
        }

        private bool TryGetPrincipal(AuthenticationHeaderValue authHeader, out IPrincipal principal)
        {
            var basicCredentials = BasicCredentials.ParseFromHeader(authHeader);
            
            if (basicCredentials != null)
            {
                var isAuthenticated = Membership.ValidateUser(basicCredentials.Username, basicCredentials.Password);

                if (isAuthenticated)
                {
                    var identity = new GenericIdentity(basicCredentials.Username);
                    var roles = System.Web.Security.Roles.GetRolesForUser(basicCredentials.Username);

                    principal = new GenericPrincipal(identity, roles); // out parameter
                    return true;
                }                
            }

            principal = null; // out parameter
            return false;
        }


        // Inner classes
        private class BasicCredentials
        {
            public string Username { get; private set; }
            public string Password { get; private set; }
            
            public static BasicCredentials ParseFromHeader(AuthenticationHeaderValue authHeader)
            {
                if (authHeader.Scheme != "Basic")
                    return null;

                var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

                if (credentials.Length != 2 || string.IsNullOrWhiteSpace(credentials[0]) || string.IsNullOrWhiteSpace(credentials[1]))
                    return null;

                return new BasicCredentials { Username = credentials[0].Trim(), Password = credentials[1].Trim() };
            }
        }
    }
}