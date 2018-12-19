using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStoreApp.DataAccess
{
    public partial class PizzaOrder
    {
        public PizzaOrder()
        {
            PizzasInOrder = new HashSet<PizzasInOrder>();
        }

        [Key]
        public int PizzaOrderId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddressId { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime DatePlaced { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("CustomerAddressId")]
        public virtual CustomerAddress CustomerAddress { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public virtual ICollection<PizzasInOrder> PizzasInOrder { get; set; }
    }
}
