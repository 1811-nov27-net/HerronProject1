using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class PizzaOrder
    {
        public PizzaOrder()
        {
            PizzasInOrder = new HashSet<PizzasInOrder>();
        }

        public int PizzaOrderId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddressId { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime DatePlaced { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<PizzasInOrder> PizzasInOrder { get; set; }
    }
}
