
namespace BeatDave.Domain
{
    using BeatDave.Domain.Infrastructure;

    public class SocializerService : IHandleEvent<EntryLoggedEvent>
    {
        public void Handle(EntryLoggedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}