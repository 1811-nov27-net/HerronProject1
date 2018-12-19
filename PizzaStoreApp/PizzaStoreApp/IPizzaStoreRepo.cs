using PizzaStoreApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    public interface IPizzaStoreRepo
    {
        CustomerClass LoadCustomerByUsername(string username);
        AddressClass LoadAddressByID(int id);
        Dictionary<int, string> GenerateIngrediantDictionary();
        IEnumerable<CustomerClass> LoadCustomerByName(string FirstName, string LastName);
        IEnumerable<StoreClass> LoadLocations();
        void AddCustomer(CustomerClass customer);
        void AddAddressToCustomer(AddressClass address, CustomerClass customer);
        void RemoveCustomerAddress(AddressClass address, CustomerClass customer);
        IEnumerable<OrderClass> LoadOrdersByLocation(StoreClass location);
        IEnumerable<OrderClass> LoadOrdersByCustomer(CustomerClass customer);
        void PlaceOrder(OrderClass order);
        int GetPizzaID(HashSet<string> Ingrediants, PizzaClass.PizzaSize size);
        void Save();
        void UpdateLocation(StoreClass location);
        void UpdateCustomer(CustomerClass customer);
        void UpdateAddress(AddressClass address);
        void AddIngrediantToList(string AdminUsername, string AdminPassword, string IngrediantName);
        void RemoveLocation(string AdminUsername, string AdminPassword, StoreClass location);
        void AddStore(string AdminUsername, string AdminPassword, StoreClass location);
        void CheckPassword(CustomerClass customer, string password);
        void ChangeUserPassword(string AdminUsername, string AdminPassword, CustomerClass customer, string NewPassword);

    }
}
