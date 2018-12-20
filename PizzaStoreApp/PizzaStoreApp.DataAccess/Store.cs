using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int StoreId { get; set; }
        [MaxLength(100)]
        public string StoreName { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(100)]
        public string Street2 { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        public int Zip { get; set; }
        [MaxLength(100)]
        public string State { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<Invantory> Invantory { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
