using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStoreApp.DataAccess
{
    public partial class Invantory
    {

        [Key]
        [Column(Order =1)]
        public int StoreId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int IngrediantId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("IngrediantId")]
        public virtual Ingrediants Ingrediant { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}
