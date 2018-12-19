using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class Pizza
    {
        public Pizza()
        {
            Ingrediants = new HashSet<Ingrediants>();
            PizzasInOrder = new HashSet<PizzasInOrder>();
        }

        public int PizzaId { get; set; }
        public int Size { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Ingrediants> Ingrediants { get; set; }
        public virtual ICollection<PizzasInOrder> PizzasInOrder { get; set; }
    }
}
