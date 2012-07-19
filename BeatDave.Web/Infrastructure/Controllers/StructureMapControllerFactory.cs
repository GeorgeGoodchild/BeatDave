using System;
using System.Web.Mvc;
using StructureMap;

namespace BeatDave.Web.Infrastructure
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) 
                return null;
            
            return (Controller)ObjectFactory.GetInstance(controllerType);
        }
    }
}