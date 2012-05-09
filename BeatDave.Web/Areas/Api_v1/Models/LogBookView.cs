using System;
using System.Collections.Generic;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class LogBookView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public UnitsView Units { get; set; }
        public List<RecordView> Records { get; set; }

        public OwnerView Owner { get; set; }
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

        public class RecordView
        {
            public int Id { get; set; }
            public double Value { get; set; }
            public DateTime OccurredOn { get; set; }
        }

        public class OwnerView
        {
            public int OwnerUsername { get; set; }
            public string OwnerFullName { get; set; }
        }

        public class SocialNetworkAccountView
        {
            public string NetworkName { get; set; }
            public string UserName { get; set; }
        }
    }
}