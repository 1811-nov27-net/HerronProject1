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
            throw new NotImplementedException();
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
