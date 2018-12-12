using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FavoriteStoreId { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
