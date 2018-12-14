using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class PizzaUI
    {
        public PizzaUI ()
        {

        }

        public double Price { get; set; }
        public HashSet<string> Ingrediants { get; set; }
        public PizzaSize Size { get; set; }
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int PizzaID { get; set; }



    }

    public enum PizzaSize
    {
        Personal = 0,
        Small,
        Medium,
        Large,
        XLarge
    }

}
