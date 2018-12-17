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

        internal static CustomerClass Map(CustomerUI customerUI)
        {
            if (customerUI == null)
                return null;
            throw new NotImplementedException();
        }

        internal static CustomerUI Map(CustomerClass user)
        {
            if (user == null)
                return null;
            throw new NotImplementedException();
        }
    }
}
