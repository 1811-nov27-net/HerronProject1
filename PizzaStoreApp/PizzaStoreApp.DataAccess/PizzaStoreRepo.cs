using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaStoreAppLibrary;

namespace PizzaStoreApp.DataAccess
{
    public class PizzaStoreRepo : IPizzaStoreRepo
    {

        private readonly PizzaStoreDBContext _db;

        /// <summary>
        /// Initializes a new Pizza Store repository given a suitable Entity Framework DbContext.
        /// </summary>
        /// <param name="db">The DbContext</param>
        public PizzaStoreRepo(PizzaStoreDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public void AddAddressToCustomer(AddressClass address, CustomerClass customer)
        {
            Customer cust = _db.Customer.Include(c => c.CustomerAddress).First(c => c.Username == customer.Username);
            cust.CustomerAddress.Add(Mapper.Map(address));
            Save();
        }

        public void AddCustomer(CustomerClass customer)
        {
            _db.Customer.Add(Mapper.Map(customer));
            Save();
        }

        public void AddIngrediantToList(string AdminUsername, string AdminPassword, string IngrediantName)
        {
            IngrediantList ig = new IngrediantList
            {
                IngrediantName = IngrediantName
            };
            _db.IngrediantList.Add(ig);
            Save();
        }

        public void AddStore(string AdminUsername, string AdminPassword, StoreClass location)
        {
            if (AdminPassword == SecretString.AdminPassword && AdminUsername == SecretString.AdminUsername)
            {
                _db.Store.Add(Mapper.Map(location));
                Save();

            }
            else
            {
                throw new InvalidLoginException();
            }
        }

        public void ChangeUserPassword(string AdminUsername, string AdminPassword, CustomerClass customer, string NewPassword)
        {
            if (AdminUsername == SecretString.AdminUsername && AdminPassword == SecretString.AdminPassword)
            {
                Customer NewCust = _db.Customer.First(c => c.Username == customer.Username);
                NewCust.Password = NewPassword;

            }
            else
            {
                throw new InvalidLoginException();
            }
        }

        public Dictionary<int, string> GenerateIngrediantDictionary()
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();
            var tempDic = _db.IngrediantList.Where(c => c.IngrediantName != null).AsNoTracking();
            foreach (var ingrediant in tempDic)
            {
                ret[ingrediant.IngrediantId] = ingrediant.IngrediantName;
            }

            return ret;
        }
        
        public IEnumerable<CustomerClass> LoadCustomerByName(string FirstName, string LastName)
        {
            return Mapper.Map(_db.Customer.Where(c => c.FirstName == FirstName && c.LastName == LastName).AsNoTracking());
        }

        public CustomerClass LoadCustomerByUsername(string username)
        {
            return Mapper.Map(_db.Customer.First(c => c.Username == username));
        }

        public IEnumerable<StoreClass> LoadLocations()
        {
            List<Store> temp = _db.Store.ToList();
            List<StoreClass> ret = new List<StoreClass>();
            foreach (var store in temp)
            {
                ret.Add(Mapper.Map(store));
            }
            return ret;
        }

        public IEnumerable<OrderClass> LoadOrdersByCustomer(CustomerClass customer)
        {
            int custID = _db.Customer.Where(c => c.Username == customer.Username).First().CustomerId;
            List<PizzaOrder> temp = _db.PizzaOrder.Where(o => o.CustomerId == custID).ToList();
            List<OrderClass> ret = new List<OrderClass>();
            foreach (var order in temp)
            {
                ret.Add(Mapper.Map(order));
            }
            return ret;
        }

        public IEnumerable<OrderClass> LoadOrdersByLocation(StoreClass location)
        {
            int locID = _db.Store.Where(s => s.StoreName == location.Name).First().StoreId;
            List<PizzaOrder> temp = _db.PizzaOrder.Where(o => o.StoreId == locID).ToList();
            List<OrderClass> ret = new List<OrderClass>();
            foreach (var order in temp)
            {
                ret.Add(Mapper.Map(order));
            }
            return ret;
        }

        public void PlaceOrder(OrderClass order)
        {
            _db.Add(Mapper.Map(order));
        }

        public void RemoveCustomerAddress(AddressClass address, CustomerClass customer)
        {
            int addID = _db.CustomerAddress.Where(a => a.Street == address.Street && a.State == address.State).First().CustomerAddressId;
            List<PizzaOrder> OldOrderList = _db.PizzaOrder.Where(o => o.CustomerAddressId == addID).ToList();
            foreach (var order in OldOrderList)
            {
                order.CustomerAddressId = 1; // one is the "deleted customer address" value
            }
            _db.Remove(_db.CustomerAddress.Where(ca => ca.Customer.Username == customer.Username && ca.Street == address.Street));
            Save();
        }

        public void RemoveLocation(string AdminUsername, string AdminPassword, StoreClass location)
        {
            if (AdminUsername == SecretString.AdminUsername && AdminPassword == SecretString.AdminPassword)
            {
                _db.Remove(_db.Store.Where(s => s.StoreName == location.Name));
                Save();
            }
            else
            {
                throw new InvalidLoginException();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateCustomer(CustomerClass customer)
        {
            _db.Entry(_db.Customer.Find(customer.Username)).CurrentValues.SetValues(Mapper.Map(customer));
            Save();
        }

        public void UpdateLocation(StoreClass location)
        {
            _db.Entry(_db.Customer.Find(location.Name)).CurrentValues.SetValues(Mapper.Map(location));
        }
    }
}
