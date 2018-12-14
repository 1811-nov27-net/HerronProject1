using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.DataAccess
{
    public class Store
    {
        [Key]
        [StringLength(100)]
        public string StoreName { get; set; }
        public int NumberOfSupremes { get; set; }
        public int NumberOfMeatLovers { get; set; }
        public int NumberOfVeggie { get; set; }
        public int NumberOfSolidGold { get; set; }
        public int Zip { get; set; }

    }
}
