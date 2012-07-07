
namespace BeatDave.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using BeatDave.Domain.Infrastructure;

    public class Entry    
    {
        // Properties
        public int Id { get; internal set; }
        public LogBook LogBook { get; internal set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
        private List<Comment<Entry>> Comments { get; set; }


        // C'tor
        public Entry()
        { }


        // Public Members
        public IEnumerable<Comment<Entry>> GetComments()
        {
            var comments = this.Comments ?? new List<Comment<Entry>>();

            return new ReadOnlyCollection<Comment<Entry>>(comments);       
        }
        
        public void AddComment(Comment<Entry> comment)
        {
            if (this.Comments == null)
                this.Comments = new List<Comment<Entry>>();

            comment.Id = this.Comments.Count == 0 ? 1 : this.Comments.Max(x => x.Id) + 1;
            comment.CommentOn = this;
            comment.CreatedOn = DateTime.UtcNow;

            this.Comments.Add(comment);
            
            DomainEvents.Raise(new CommentMadeEvent<Entry>(comment));
        }

        public void RemoveComment(Comment<Entry> comment)
        {
            if (this.Comments == null)
                return;

            this.Comments.Remove(comment);
        }
    }
}