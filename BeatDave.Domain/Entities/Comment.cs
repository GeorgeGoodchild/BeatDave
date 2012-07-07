
namespace BeatDave.Domain
{
    using System;

    public abstract class Comment
    {
        public int Id { get; internal set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; internal set; }
        public string CreatedBy { get; set; }
    }

    public class Comment<T> : Comment
    {
        public T CommentOn { get; internal set; }        
    }
}
