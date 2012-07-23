using System.Collections.Generic;
using BeatDave.Domain;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class LogBookView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public UnitsView Units { get; set; }
        public List<EntryView> Entries { get; set; }

        public string Owner { get; set; }
        public List<SocialNetworkAccountView> AutoShareOn { get; set; }
        public Visibility Visibility { get; set; }

        //
        // Nested Inner Classes
        //
        public class UnitsView
        {
            public string Symbol { get; set; }
            public SymbolPosition SymbolPosition { get; set; }
            public int Precision { get; set; }
        }

        public class EntryView
        {
            public int Id { get; set; }
            public double Value { get; set; }
            public DateTimeView OccurredOn { get; set; }
            public List<CommentView> Comments { get; set; }
        }

        public class CommentView
        {
            public string Text { get; set; }
            public DateTimeView CreatedOn { get; internal set; }
            public string CreatedBy { get; set; }
        }

        public class SocialNetworkAccountView
        {
            public string NetworkName { get; set; }
        }
    }
}