﻿using System;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class EntryInput
    {
        [Obsolete] 
        public int LogBookId { get; set; }
        
        [Obsolete] 
        public int Id { get; set; }
        
        public double Value { get; set; }
        
        public DateTime OccurredOn { get; set; }
    }
}