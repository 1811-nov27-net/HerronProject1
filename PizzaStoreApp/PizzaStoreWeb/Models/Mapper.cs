using PizzaStoreApp;
using PizzaStoreAppLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStoreWeb.Models
{
    public class Mapper
    {
        internal static AddressClass Map(AddressUI address)
        {
            if (address == null)
                return null;
            return new AddressClass()
            {
                Street = address.Street,
                Apartment = address.Apartment,
                City = address.City,
                Zip = address.Zip,
                State = address.State,
                AddressID = address.AddressID,
                StoreID = address.StoreID,
                CustomerID = address.CustomerID

            };
        }

        internal static AddressUI Map(AddressClass address)
        {
            if (address == null)
            {
                return null;
            }
            return new AddressUI()
            {
                Street = address.Street,
                Apartment = address.Apartment,
                City = address.City,
                Zip = address.Zip,
                State = address.State,
                AddressID = address.AddressID,
                StoreID = address.StoreID,
                CustomerID = address.CustomerID

            };
        }

        internal static CustomerClass Map(CustomerUI customer)
        {
            if (customer == null)
                return null;
            CustomerClass cust = new CustomerClass()
            {
                Username = customer.Username,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FavoriteStoreID = customer.FavoriteStoreID,
                FailedPasswordChecks = customer.FailedPasswordChecks,
                UserID = customer.UserID
            };

            foreach (var add in customer.Addresses)
            {
                cust.Addresses.Add(Map(add));
            }

            foreach (var order in customer.Orders)
            {
                cust.PreviousOrders.Add(Map(order));
            }

            return cust;
        }

        internal static CustomerUI Map(CustomerClass customer)
        {
            if (customer == null)
                return null;
            CustomerUI cust = new CustomerUI()
            {
                Username = customer.Username,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FavoriteStoreID = customer.FavoriteStoreID,
                FailedPasswordChecks = customer.FailedPasswordChecks,
                UserID = customer.UserID
            };

            foreach (var add in customer.Addresses)
            {
                cust.Addresses.Add(Map(add));
            }

            foreach (var order in customer.PreviousOrders)
            {
                cust.Orders.Add(Map(order));
            }

            cust.SuggestedOrder = Map(customer.SuggestOrder());

            return cust;
        }

        internal static StoreClass Map(StoreUI Store)
        {
            if (Store == null)
                return null;
            return new StoreClass
            {
                StoreID = Store.StoreID,
                Name = Store.Name,
                Invantory = Store.Invantory,
                Address = Map(Store.Address)
            };
        }

        internal static StoreUI Map(StoreClass Store)
        {
            if (Store == null)
                return null;
            return new StoreUI
            {
                StoreID = Store.StoreID,
                Name = Store.Name,
                Invantory = Store.Invantory,
                Address = Map(Store.Address)
            };
        }


        internal static OrderClass Map(OrderUI Order)
        {
            if (Order == null)
                return null;
            OrderClass ret = new OrderClass()
            {
                OrderID = Order.OrderID,
                DeliveryAddress = Map(Order.DeliveryAddress),
                Store = Map(Order.Store),
                Customer = Map(Order.Customer),
                DatePlaced = Order.DatePlaced
            };
            foreach (var zaa in Order.pizzas)
            {
                ret.pizzas.Add(Map(zaa));
            }
            ret.UpdateTotal();
            return ret;
        }

        internal static OrderUI Map(OrderClass Order)
        {
            if (Order == null)
                return null;
            OrderUI ret = new OrderUI()
            {
                OrderID = Order.OrderID,
                DeliveryAddress = Map(Order.DeliveryAddress),
                Store = Map(Order.Store),
                Customer = Map(Order.Customer),
                CostBeforeTax = Order.CostBeforeTax,
                TotalCost = Order.TotalCost,
                DatePlaced = Order.DatePlaced
            };
            foreach (var zaa in Order.pizzas)
            {
                ret.pizzas.Add(Map(zaa));
                ret.pizzaIDs.Add(zaa.PizzaID);
            }
            return ret;
        }

        internal static PizzaClass Map(PizzaUI Pizza)
        {
            if (Pizza == null)
                return null;
            PizzaClass zaa = new PizzaClass(Pizza.Size, Pizza.Ingrediants);
            if (Pizza.PizzaID != 0)
                zaa.PizzaID = Pizza.PizzaID;
            return zaa;
        }

        internal static PizzaUI Map(PizzaClass Pizza)
        {
            if (Pizza == null)
                return null;
            return new PizzaUI
            {
                Size = Pizza.Size,
                Ingrediants = Pizza.Ingrediants,
                PizzaID = Pizza.PizzaID,

            };
        }

    }
}
