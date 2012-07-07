using System;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class CommentInput
    {
        [Obsolete] 
        public int LogBookId { get; set; }
        
        [Obsolete] 
        public int EntryId { get; set; }
        
        public string Text { get; set; }
    }
}