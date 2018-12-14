using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class OrderUI
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }
        public AddressUI DeliveryAddress { get; set; }
        public List<PizzaUI> pizzas;
        public StoreUI Store { get; set; }
        public CustomerUI Customer { get; set; }
        [Display(Name ="Subtotal")]
        public double CostBeforeTax { get; set; }
        [Display(Name = "Total")]
        public double TotalCost { get; set; }
        [Display(Name ="Date Placed")]
        public DateTime DatePlaced { get; set; }

        public OrderUI() { }

    }
}
