using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class IngrediantsOnPizza
    {
        public int PizzaId { get; set; }
        public int IngrediantId { get; set; }

        public virtual IngrediantList Ingrediant { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
