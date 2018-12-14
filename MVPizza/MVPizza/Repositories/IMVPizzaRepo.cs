using MVPizza.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPizza.Repositories
{
    interface IMVPizzaRepo
    {
        Order GetOrderByID(int OrderID);
        Store GetStoreByName(string StoreName);
        User GetUserByUsername(string Username);
        Address GetAddressById(int AddressID);
        IList<Order> GetOrdersByUsername(string Username);
        IList<Order> GetOrdersByStore(string StoreName);
        IList<Order> GetOrdersByAddress(int AddressID);
        Store GetStoreByZip(int Zip);
        IList<Address> GetAddressesByUsername(string Username);
        User GetUserByName(string FirstName, string LastName = null);
        void PlaceOrder(Order order);
        void AddAddressToUser(Address address, User user);
    }
}
