using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class AddressUI
    {
        public AddressUI()
        {

        }

        [StringLength(100)]
        public string Street { get; set; }
        [StringLength(100)]
        public string Apartment { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        public int Zip { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int AddressID { get; set; }

        public int StoreID { get; set; }

    }
}
