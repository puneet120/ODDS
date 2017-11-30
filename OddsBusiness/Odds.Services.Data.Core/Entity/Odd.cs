using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddsBusiness.Core.Entity
{
    public class Odd : BaseEntity
    {
        public string Description { get; set; }

        public decimal? Odd_1 { get; set;}

        public decimal? Odd_X { get; set; }

        public decimal? Odd_2 { get; set; }
    }
}
