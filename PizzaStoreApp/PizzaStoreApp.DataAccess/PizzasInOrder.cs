using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStoreApp.DataAccess
{
    public partial class PizzasInOrder
    {
        [Key]
        [Column(Order = 1)]
        public int PizzaId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PizzaOrderId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("PizzaId")]
        public virtual Pizza Pizza { get; set; }
        [ForeignKey("PizzaOrderId")]
        public virtual PizzaOrder PizzaOrder { get; set; }
    }
}
