using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using BeatDave.Web.Infrastructure;
using DataAnnotationsExtensions.ClientValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StructureMap;

namespace BeatDave.Web
{
    public class WebApiApplication : HttpApplication
    {        
        // C'tor
        public WebApiApplication()
        { }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterFormatters(GlobalConfiguration.Configuration);
            
            Bootstrapper.Bootstrap();
            AutoMapperConfiguration.Configure();
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());  
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver();
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }



        // Helpers
        private void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        private void RegisterFormatters(HttpConfiguration config)
        {
            //
            // Format dates properly rather than with the ridiculous Unix 1970 weird thing
            //
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IsoDateTimeConverter());
            settings.Converters.Add(new StringEnumConverter());

            var formatter = (JsonMediaTypeFormatter)config.Formatters[0];
            formatter.SerializerSettings = settings;
        }
    }
}