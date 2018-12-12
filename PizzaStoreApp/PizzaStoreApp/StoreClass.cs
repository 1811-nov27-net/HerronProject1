using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    public class StoreClass
    {
        public string Name { get; set; }
        public AddressClass Address { get; set; }
        public Dictionary<string, int> Invantory = new Dictionary<string, int>();

        public StoreClass(string givenName)
        {
            Name = givenName;
            Restock();
        }

        public void Restock()
        {
            Invantory.Clear();
            foreach (string item in OrderClass.Ingrediants)
            {
                Invantory.Add(item, 30);
            }

        }

        public bool ServeOrder(OrderClass order)
        {
            Dictionary<string, int> ingrediantsNeeded = new Dictionary<string, int>();
            foreach (string item in OrderClass.Ingrediants)
            {
                ingrediantsNeeded.Add(item, 0);
            }
            foreach (PizzaClass pizza in order.pizzas)
            {
                foreach (string item in pizza.Ingrediants)
                {
                    ingrediantsNeeded[item]++;
                }
            }
            foreach (string item in OrderClass.Ingrediants)
            {
                if (Invantory[item] < ingrediantsNeeded[item])
                    return false;
            }
            foreach (string item in OrderClass.Ingrediants)
            {
                Invantory[item] -= ingrediantsNeeded[item];
            }
            order.Store = Name;
            return true;
        }



    }
}
