﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.DataAccess
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [StringLength(100)]
        [ForeignKey("User")]
        [Display(Name = "Customer Username")]
        public string Username { get; set; }
        [StringLength(100)]
        [ForeignKey("Store")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        [Display(Name = "Time Placed")]
        public DateTime TimePlaced { get; set; }
        [Display(Name = "Supreme Pizzas")]
        public int NumberOfSupremes { get; set; }
        [Display(Name = "Meat Lover Pizzas")]
        public int NumberOfMeatLovers { get; set; }
        [Display(Name = "Vegeterian Pizzas")]
        public int NumberOfVeggie { get; set; }
        [Display(Name = "Solid Gold Pizzas")]
        public int NumberOfSolidGold { get; set; }
        [Display(Name = "Total Cost of Order")]
        double TotalCost { get; set; }
        public Address DeliveryAddress { get; set; }
        public Store Store { get; set; }
        public User User { get; set; }

        public static double CostOfSupreme = 10;
        public static double CostOfMeatLovers = 8;
        public static double CostOfVeggie = 7;
        public static double CostOfSolidGold = 100;

        public bool VerifyOrder(DateTime lastOrder)
        {
            if (lastOrder == null)
            {
                lastOrder = DateTime.Now.AddDays(-1);
            }
            if (DateTime.Now.Hour - lastOrder.Hour > 2)
                return false;
            if (NumberOfMeatLovers + NumberOfSolidGold + NumberOfSupremes + NumberOfVeggie > 12)
                return false;
            TotalCost = NumberOfMeatLovers * CostOfMeatLovers + NumberOfSolidGold * CostOfSolidGold + NumberOfSupremes * CostOfSupreme + NumberOfVeggie * CostOfVeggie;
            if (TotalCost > 500)
                return false;
            return true;
        }


        public Order() { }

    }
}
