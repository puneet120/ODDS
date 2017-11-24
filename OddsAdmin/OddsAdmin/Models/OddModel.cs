using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddsAdmin.Models
{
    public class OddModel:BaseModel
    {
        
        [Required]
        [StringLength(5000)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Invalid Odd 1")]
        public decimal? Odd_1 { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Invalid Odd X")]
        public decimal? Odd_X { get; set; }

        [Required]
        [Range(0, Int64.MaxValue, ErrorMessage = "Invalid Odd 2")]
        public decimal? Odd_2 { get; set; }

    }
}