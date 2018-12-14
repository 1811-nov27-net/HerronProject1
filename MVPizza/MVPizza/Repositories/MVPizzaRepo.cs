using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVPizza.Models;

namespace MVPizza.Repositories
{
    public class MVPizzaRepo : IMVPizzaRepo
    {
        public void AddAddressToUser(Address address, User user)
        {
            throw new NotImplementedException();
        }

        public Address GetAddressById(int AddressID)
        {
            throw new NotImplementedException();
        }

        public IList<Address> GetAddressesByUsername(string Username)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int OrderID)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetOrdersByAddress(int AddressID)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetOrdersByStore(string StoreName)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetOrdersByUsername(string Username)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreByName(string StoreName)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreByZip(int Zip)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string FirstName, string LastName = null)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string Username)
        {
            throw new NotImplementedException();
        }

        public void PlaceOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
