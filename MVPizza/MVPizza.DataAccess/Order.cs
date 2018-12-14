using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.DataAccess
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string StoreName { get; set; }
        public int AddressID { get; set; }
        public DateTime TimePlaced { get; set; }

        public Order() { }

    }
}
