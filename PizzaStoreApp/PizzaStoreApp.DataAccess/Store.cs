using System;
using System.Collections.Generic;

namespace PizzaStoreApp.DataAccess
{
    public partial class Store
    {
        public Store()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            Invantory = new HashSet<Invantory>();
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<Invantory> Invantory { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
