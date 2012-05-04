using System;
using System.Web;

namespace BeatDave.Web.Infrastructure
{
    // Problem:    401's returned from the API are being redirected to the Forms Auth logon page
    // Provenance: http://haacked.com/archive/2011/10/04/prevent-forms-authentication-login-page-redirect-when-you-donrsquot-want.aspx
    //
    public class SuppressFormsAuthenticationRedirectModule : IHttpModule
    {
        // Static Properties
        private static readonly object SuppressAuthenticationKey = new Object();


        // Static Members
        public static void SuppressAuthenticationRedirect(HttpContext context)
        {
            context.Items[SuppressAuthenticationKey] = true;
        }

        public static void SuppressAuthenticationRedirect(HttpContextBase context)
        {
            context.Items[SuppressAuthenticationKey] = true;
        }


        // C'tor
        public SuppressFormsAuthenticationRedirectModule()
        { }


        // Public Members
        public void Init(HttpApplication context)
        {
            context.PostReleaseRequestState += OnPostReleaseRequestState;
            context.EndRequest += OnEndRequest;
        }

        public void Dispose()
        {
        }


        // Private Event Handler Members
        private void OnPostReleaseRequestState(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;
            var request = context.Request;

            if (response.StatusCode == 401 && request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                SuppressAuthenticationRedirect(context.Context);
            }
        }

        private void OnEndRequest(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;

            if (context.Context.Items.Contains(SuppressAuthenticationKey))
            {
                response.TrySkipIisCustomErrors = true;
                response.ClearContent();
                response.StatusCode = 401;
                response.RedirectLocation = null;
            }
        }
    }
}

