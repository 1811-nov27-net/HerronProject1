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
        public int StoreID { get; set; }


        public void Restock(string[] ingrediants, int[] amounts)
        {
            for (int i = 0; i < ingrediants.Length; i++)
            {
                Invantory[ingrediants[i]] = amounts[i];
            }
        }

        public bool ServeOrder(OrderClass order)
        {
            Dictionary<string, int> ingrediantsNeeded = new Dictionary<string, int>();
            foreach (PizzaClass pizza in order.pizzas)
            {
                foreach (string item in pizza.Ingrediants)
                {
                    if (ingrediantsNeeded.ContainsKey(item))
                        ingrediantsNeeded[item]++;
                    else if (Invantory.ContainsKey(item) && Invantory[item] > 0)
                        ingrediantsNeeded.Add(item, 1);
                    else
                        throw new OutOfStockException(item);
                }
            }
            order.Store = this;
            return true;
        }



    }
}
