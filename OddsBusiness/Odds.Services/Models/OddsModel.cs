using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddsBusiness.Models
{
    public class OddsModel:BaseModel
    {

        public string Description { get; set; }

        public string Odd_1 { get; set; }

        public string  Odd_X { get; set; }

        public string Odd_2 { get; set; }

    }
}