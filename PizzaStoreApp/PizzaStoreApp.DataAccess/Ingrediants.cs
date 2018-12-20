using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreApp.DataAccess
{
    public partial class Ingrediants
    {
        public Ingrediants()
        {
            Pizzas = new HashSet<Pizza>();
            Invantory = new HashSet<Invantory>();
        }

        [Key]
        public int IngrediantId { get; set; }
        [Required]
        [MaxLength(100)]
        public string IngrediantName { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Invantory> Invantory { get; set; }
    }
}
