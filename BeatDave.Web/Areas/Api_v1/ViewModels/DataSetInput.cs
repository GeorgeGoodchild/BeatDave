using System;
using System.Collections.Generic;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Contracts
{
    public class DataPointContract
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime OccurredOn { get; set; }
    }
    
    public class DataSetContract
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UnitsContract Units { get; set; }
        public string OwnerId { get; set; }
        public Visibility Visibility { get; set; }
    }
    
    public class DenormalizedReference
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }
    
    public class UnitsContract
    {
        public string Symbol { get; set; }
        public SymbolPosition SymbolPosition { get; set; }
        public int Precision { get; set; }
    }
    
    public class UserContract
    {
    }
}