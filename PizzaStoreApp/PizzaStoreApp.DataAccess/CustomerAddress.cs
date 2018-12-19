using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStoreApp.DataAccess
{
    public partial class CustomerAddress
    {
        public CustomerAddress()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        [Key]
        public int CustomerAddressId { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
