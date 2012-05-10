using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using BeatDave.Web.Infrastructure;
using DataAnnotationsExtensions.ClientValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.MvcIntegration;

namespace BeatDave.Web
{    
    public class WebApiApplication : HttpApplication
    {
        // Static Properties
        public static IDocumentStore DocumentStore { get; private set; }


        // C'tor
        public WebApiApplication()
        {
            BeginRequest += (sender, args) =>
            {
                HttpContext.Current.Items["CurrentRequestRavenSession"] = FatController.DocumentStore.OpenSession();
            };
            EndRequest += (sender, args) =>
            {
                using (var session = (IDocumentSession)HttpContext.Current.Items["CurrentRequestRavenSession"])
                {
                    if (session == null)
                        return;

                    if (Server.GetLastError() != null)
                        return;

                    session.SaveChanges();
                }
                //TaskExecutor.StartExecuting();
            };
        }

        protected void Application_Start()
        {
            LogManager.GetCurrentClassLogger().Info("Started application");

            RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterFormatters(GlobalConfiguration.Configuration);

            InitializeDocumentStore();
            FatController.DocumentStore = DocumentStore;
            FatApiController.DocumentStore = DocumentStore;
            //TaskExecutor.DocumentStore = DocumentStore;

            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
        }


        // Helpers
        private void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
        }

        private void RegisterFormatters(HttpConfiguration config)
        {
            //
            // Format dates properly rather than with the ridiculous Unix 1970 weird thing
            //
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IsoDateTimeConverter());
            settings.Converters.Add(new StringEnumConverter());

            //
            // Setting it at position [0] makes it the default formatter and will override the DataContratSerializer for JSON messages
            // 
            config.Formatters[0] = new JsonNetFormatter(settings);
        }

        private void InitializeDocumentStore()
        {
            DocumentStore = new DocumentStore { ConnectionStringName = "BeatDave" }.Initialize();

            TryCreatingIndexesOrRedirectToErrorPage();

            RavenProfiler.InitializeFor(DocumentStore);
        }

        private void TryCreatingIndexesOrRedirectToErrorPage()
        {
            try
            {
                IndexCreation.CreateIndexes(typeof(WebApiApplication).Assembly, DocumentStore);
            }
            catch (WebException e)
            {
                var socketException = e.InnerException as SocketException;
                if (socketException == null)
                    throw;

                switch (socketException.SocketErrorCode)
                {
                    case SocketError.AddressNotAvailable:
                    case SocketError.NetworkDown:
                    case SocketError.NetworkUnreachable:
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                    case SocketError.TimedOut:
                    case SocketError.ConnectionRefused:
                    case SocketError.HostDown:
                    case SocketError.HostUnreachable:
                    case SocketError.HostNotFound:
                        HttpContext.Current.Response.Redirect("Views/500.htm");
                        break;
                    default:
                        throw;
                }
            }
        }
    }
}