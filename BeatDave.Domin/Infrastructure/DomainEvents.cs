
namespace BeatDave.Domain.Infrastructure
{
    using System.Diagnostics;
    using StructureMap;

    public interface IEvent
    { }

    public interface IHandleEvent<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }

    public static class DomainEvents
    {
        [DebuggerStepThrough]
        public static void Raise<TEvent>(TEvent @event) where TEvent : IEvent
        {
            foreach (var handler in ObjectFactory.GetAllInstances<IHandleEvent<TEvent>>())
            {
                handler.Handle(@event);
            }
        }
    }
}