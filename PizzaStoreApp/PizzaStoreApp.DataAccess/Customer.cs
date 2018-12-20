using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreApp.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            PizzaOrder = new HashSet<PizzaOrder>();
        }
        [Key]
        public int CustomerId { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MinLength(25, ErrorMessage ="Password must be at least 25 characters long")]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public int? FavoriteStoreId { get; set; }
        public int FailedPasswordChecks { get; set; }


        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
