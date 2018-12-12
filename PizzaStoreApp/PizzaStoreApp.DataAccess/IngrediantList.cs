using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class IngrediantList
    {
        public IngrediantList()
        {
            IngrediantsOnPizza = new HashSet<IngrediantsOnPizza>();
            Invantory = new HashSet<Invantory>();
        }

        public int IngrediantId { get; set; }
        public string IngrediantName { get; set; }

        public virtual ICollection<IngrediantsOnPizza> IngrediantsOnPizza { get; set; }
        public virtual ICollection<Invantory> Invantory { get; set; }
    }
}
