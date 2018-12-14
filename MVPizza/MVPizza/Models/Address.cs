using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string StoreName { get; set; }
        [StringLength(200)]
        public string Street { get; set; }
        public int Zip { get; set; }

    }
}
