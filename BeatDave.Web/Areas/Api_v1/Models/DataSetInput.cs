using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeatDave.Web.Models;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class DataSetInput
    {        
        public string Id { get; set; }        
        
        [Required]
        public string Title { get; set; }        
        
        public string Description { get; set; }        
        
        public UnitsInput Units { get; set; }
        
        [Required]        
        public Visibility Visibility { get; set; }

        //
        // Nested Inner Classes
        //
        public class UnitsInput
        {
            [Required]
            public string Symbol { get; set; }
            
            [Required]
            public SymbolPosition SymbolPosition { get; set; }
            
            [Required]
            [Max(6)]
            public int Precision { get; set; }
        }
    }    
}