
namespace BeatDave.Domain
{
    using System;

    public class Comment<T>
    {
        public T CommentOn { get; internal set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; internal set; }
        public string CreatedBy { get; set; }
    }
}
