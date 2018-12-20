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
            throw new NotImplementedException();
        }

        internal static CustomerUI Map(CustomerClass customer)
        {
            if (customer == null)
                return null;
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static OrderUI Map(OrderClass Order)
        {
            if (Order == null)
                return null;
            throw new NotImplementedException();
        }

        internal static PizzaClass Map(PizzaUI Pizza)
        {
            if (Pizza == null)
                return null;
            throw new NotImplementedException();
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
