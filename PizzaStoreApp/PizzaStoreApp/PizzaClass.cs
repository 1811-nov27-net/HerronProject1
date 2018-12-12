using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    public class PizzaClass
    {
        private double _price;
        public double Price { get { return _price; } }
        public HashSet<string> Ingrediants { get; set; }
        public PizzaSize Size { get; set; }

        public PizzaClass (PizzaSize size, HashSet<string> ingrediants)
         {
            Size = size;
            foreach (string item in ingrediants)
            {
                Ingrediants.Add(item);
            }
            UpdatePrice();
         }


        public double UpdatePrice()
        {
            _price = 0;
            switch (Size)
            {
                case PizzaSize.Personal:
                    _price = 3;
                    break;
                case PizzaSize.Small:
                    _price = 5;
                    break;
                case PizzaSize.Medium:
                    _price = 7;
                    break;
                case PizzaSize.Large:
                    _price = 10;
                    break;
                case PizzaSize.XLarge:
                    _price = 13;
                    break;
                default:
                    break;
            }

            foreach (string item in Ingrediants)
                _price += OrderClass.PricePerIngrediant;
            return _price;
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
}
