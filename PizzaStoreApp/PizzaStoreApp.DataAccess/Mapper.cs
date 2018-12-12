using PizzaStoreAppLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaStoreApp.DataAccess
{
    class Mapper
    {
        public static Pizza Map(PizzaClass pizzaClass)
        {
            Pizza pizza = new Pizza
            {
                Size = (int)pizzaClass.Size,
                Cost = (decimal)pizzaClass.Price
            };

            return pizza;
        }
        public static PizzaClass Map(Pizza pizza, Dictionary<int, string> IngrediantDictionary)
        {
            HashSet<string> ingrediants = new HashSet<string>();
            foreach (var IoP in pizza.IngrediantsOnPizza)
            {
                ingrediants.Add(IngrediantDictionary[IoP.IngrediantId]);
            }
            PizzaClass pizzaClass = new PizzaClass((PizzaClass.PizzaSize) pizza.Size,ingrediants);
            pizza.Cost = (decimal)pizzaClass.Price;

            return pizzaClass;
        }

        internal static Store Map(StoreClass location)
        {
            throw new NotImplementedException();
        }

        internal static CustomerAddress Map(AddressClass address)
        {
            throw new NotImplementedException();
        }

        internal static Customer Map(CustomerClass customer)
        {
            throw new NotImplementedException();
        }

        internal static List<CustomerClass> Map(IQueryable<Customer> queryable)
        {
            List<CustomerClass> ret = new List<CustomerClass>();
            foreach (var cust in queryable)
            {

                ret.Add(Map(cust));
            }
            return ret;
        }

        internal static CustomerClass Map(Customer cust)
        {
            CustomerClass ret = new CustomerClass(cust.Username, cust.Password)
            {
                FirstName = cust.FirstName,
                LastName = cust.LastName,
            };
            foreach (var Order in cust.PizzaOrder)
            {
                ret.PreviousOrders.Add(Map(Order));
            }

            return ret;
        }

        internal static StoreClass Map(Store store)
        {
            StoreClass ret = new StoreClass(store.StoreName)
            {
                
            };

            return ret;
        }

        internal static OrderClass Map(PizzaOrder order)
        {
            throw new NotImplementedException();
        }

        internal static PizzaOrder Map(OrderClass order)
        {
            throw new NotImplementedException();
        }
    }
}
