using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class StoreUI
    {
        [StringLength(100)]
        public string Name { get; set; }
        public AddressUI Address { get; set; }
        public Dictionary<string, int> Invantory = new Dictionary<string, int>();
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int StoreID { get; set; }

    }
}
