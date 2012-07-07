
namespace BeatDave.Domain
{
    using BeatDave.Domain.Infrastructure;

    public class EntryLoggedEvent : IEvent
    {
        public LogBook LogBook { get; private set; }
        public Entry Entry { get; private set; }

        // C'tor
        public EntryLoggedEvent(LogBook logBook, Entry entry)
        {
            this.LogBook = logBook;
            this.Entry = entry;
        }
    }
}