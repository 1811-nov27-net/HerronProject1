using System;
using System.Collections.Generic;
using System.Text;
using PizzaStoreApp;
using PizzaStoreAppLibrary;

namespace PizzaStoreAppLibrary
{
    public class OrderClass
    {
        public static double PricePerIngrediant = 1.00;
        public static double DeliveryFee = 2.00;
        public static double TaxRate = .08;
        public string Store { get; set; }
        public string User { get; set; }
        public AddressClass DeliveryAddress { get; set; }
        public List<PizzaClass> pizzas;
        private double _totalCost;
        private double _costBeforeTax;
        public double CostBeforeTax { get { return _costBeforeTax; } }
        public double TotalCost { get { return _totalCost; } }
        public DateTime DatePlaced;
        public int OrderID { get; set; }
        

        public void AddPizza (PizzaClass.PizzaSize size, HashSet<string> ingrediants)
        {
            if (pizzas.Count > 12)
            {
                throw new OrderTooLargeException();
            }
            PizzaClass pizza = new PizzaClass(size, ingrediants);
            pizzas.Add(pizza);
            UpdateTotal();
        }

        public double UpdateTotal()
        {
            _totalCost = 0;
            foreach (PizzaClass pizza in pizzas)
            {
                _totalCost += pizza.Price;
            }
            _costBeforeTax = _totalCost;
            _totalCost *= (1+TaxRate); // tax
            _totalCost += DeliveryFee; // delivery fee
            if (_totalCost > 500)
                throw new OrderTooExpensiveException();
            return _totalCost;
        }



        public void RemovePizza(int i)
        {
            pizzas.RemoveAt(i);
            UpdateTotal();
        }
    }

}
