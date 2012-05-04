using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class DataSetInput
    {
        [Obsolete] public int DataSetId { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public List<string> Tags { get; set; }

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
            [Range(0, 6)]
            public int Precision { get; set; }
        }
    }    
}