using PizzaStoreAppLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaStoreApp
{
    public class CustomerClass
    {
        public string Username { get; set; }
        public List<AddressClass> Addresses { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FavoriteStoreID { get; set; }
        public List<OrderClass> PreviousOrders = new List<OrderClass>();
        public int UserID { get; set; }
        public int FailedPasswordChecks { get; set; }



        public OrderClass SuggestOrder()
        {
            if (PreviousOrders.Count == 0)
            {
                return null;
            }
            else
            {
                OrderClass suggestion =  PreviousOrders.OrderByDescending(o => o.TotalCost).First();
                suggestion.OrderID = 0;
                return suggestion;
            }
        }

        
    }
}
