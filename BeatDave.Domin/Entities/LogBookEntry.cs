
namespace BeatDave.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using BeatDave.Domain.Infrastructure;

    public class LogBookEntry    
    {
        // Properties
        public LogBook LogBook { get; internal set; }
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
        private List<Comment<LogBookEntry>> Comments { get; set; }


        // C'tor
        public LogBookEntry()
        { }


        // Public Members
        public IEnumerable<Comment<LogBookEntry>> GetComments()
        {
            var comments = this.Comments ?? new List<Comment<LogBookEntry>>();

            return new ReadOnlyCollection<Comment<LogBookEntry>>(comments);       
        }
        
        public void AddComment(Comment<LogBookEntry> comment)
        {
            if (this.Comments == null)
                this.Comments = new List<Comment<LogBookEntry>>();

            comment.CommentOn = this;
            comment.CreatedOn = DateTime.UtcNow;

            this.Comments.Add(comment);
            

            DomainEvents.Raise(new CommentMadeEvent<LogBookEntry>(comment));
        }
    }
}