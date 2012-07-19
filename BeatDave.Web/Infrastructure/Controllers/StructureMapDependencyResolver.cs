using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace BeatDave.Web.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;

            if (serviceType.IsAbstract || serviceType.IsInterface)
                return ObjectFactory.TryGetInstance(serviceType);

            return ObjectFactory.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
                return new object[] {};

            return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        { }
    }
}