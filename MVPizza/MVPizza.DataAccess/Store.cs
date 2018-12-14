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
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        [Display(Name = "Invantory: Supreme")]
        public int NumberOfSupremes { get; set; }
        [Display(Name = "Invantory: Meat Lovers")]
        public int NumberOfMeatLovers { get; set; }
        [Display(Name = "Invantory: Vegeterian")]
        public int NumberOfVeggie { get; set; }
        [Display(Name = "Invantory: Solid Gold")]
        public int NumberOfSolidGold { get; set; }
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        ICollection<Order> Orders { get; set; }
        ICollection<Address> Addresses { get; set; }

    }
}
