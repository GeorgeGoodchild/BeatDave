
namespace BeatDave.Domain
{
    using BeatDave.Domain.Infrastructure;

    public class CommentMadeEvent<T> : IEvent
    {
        // Public Members
        public Comment<T> Comment { get; private set; }

        // C'tor
        public CommentMadeEvent(Comment<T> comment)
        {
            this.Comment = comment;
        }
    }
}