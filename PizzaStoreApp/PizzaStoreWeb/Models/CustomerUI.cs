using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class CustomerUI
    {
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int? FavoriteStoreID { get; set; }
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }
        public int FailedPasswordChecks { get; set; }

        public OrderUI SuggestedOrder { get; set; }

        public List<AddressUI> Addresses { get; set; }
        public List<OrderUI> Orders { get; set; }

    }
}
