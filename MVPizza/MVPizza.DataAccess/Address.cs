using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.DataAccess
{
    public class Address
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int AddressID { get; set; }
        [StringLength(100)]
        [ForeignKey("User")]
        public string Username { get; set; }
        [StringLength(100)]
        [ForeignKey("Store")]
        public string StoreName { get; set; }
        [StringLength(200)]
        public string Street { get; set; }
        public int Zip { get; set; }
        public User User { get; set; }

        ICollection<Order> Orders { get; set; }


    }
}
