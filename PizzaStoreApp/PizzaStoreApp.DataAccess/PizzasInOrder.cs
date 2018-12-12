using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class PizzasInOrder
    {
        public int PizzaId { get; set; }
        public int PizzaOrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual PizzaOrder PizzaOrder { get; set; }
    }
}
