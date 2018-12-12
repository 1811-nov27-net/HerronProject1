using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class Invantory
    {
        public int StoreId { get; set; }
        public int IngrediantId { get; set; }
        public int Quantity { get; set; }

        public virtual IngrediantList Ingrediant { get; set; }
        public virtual Store Store { get; set; }
    }
}
