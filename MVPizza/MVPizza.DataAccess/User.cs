using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVPizza.DataAccess
{
    public class User
    {
        [Key]
        [StringLength(100)]

        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }

    }
}
