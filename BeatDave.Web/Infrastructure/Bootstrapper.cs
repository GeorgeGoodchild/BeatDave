using System;
using System.Security.Principal;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.MvcIntegration;
using StructureMap;

namespace BeatDave.Web.Infrastructure
{ 
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {            
            var documentStore = new DocumentStore { ConnectionStringName = "BeatDave" }.Initialize();

            IndexCreation.CreateIndexes(typeof(WebApiApplication).Assembly, documentStore);
            RavenProfiler.InitializeFor(documentStore);
            
            ObjectFactory.Initialize(i =>
            {
                i.For<Func<IPrincipal>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x => () => HttpContext.Current.User);

                i.For<IDocumentStore>().Use(documentStore);

                i.For<IDocumentSession>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x =>
                    {
                        var store = x.GetInstance<IDocumentStore>();
                        return store.OpenSession();
                    });
            });            
        }
    }
}