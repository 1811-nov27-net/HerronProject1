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
            address.StoreID = _db.Store.Where(s => s.Zip == address.Zip).First().StoreId;
            cust.CustomerAddress.Add(Map(address,customer.UserID));
            Save();
        }

        public void AddCustomer(CustomerClass customer)
        {
            _db.Customer.Add(Map(customer));
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
                _db.Store.Add(Map(location));
                Save();

            }
            else
            {
                throw new InvalidLoginException();
            }
        }

        public CustomerClass PopulateOrderHistory(CustomerClass customer)
        {
            Customer cust = _db.Customer
                .Include(c => c.PizzaOrder)
                    .ThenInclude(po => po.PizzasInOrder)
                        .ThenInclude(PiO => PiO.Pizza)
                .Include(c=> c.PizzaOrder)
                    .ThenInclude(po => po.Store)
                        .ThenInclude(s => s.Invantory)
                .Where(c => c.CustomerId == customer.UserID).First();

            foreach (var Order in cust.PizzaOrder)
            {
                customer.PreviousOrders.Add(Map(Order, customer, Map(Order.Store)));
            }

            return customer;
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
            return Map(_db.Customer.Where(c => c.FirstName == FirstName && c.LastName == LastName).AsNoTracking());
        }

        public CustomerClass LoadCustomerByUsername(string username)
        {
            return Map(_db.Customer.First(c => c.Username == username));
        }

        public IEnumerable<StoreClass> LoadLocations()
        {
            List<Store> temp = _db.Store.Include(s=>s.Invantory).ThenInclude(i => i.Ingrediant).ToList();
            List<StoreClass> ret = new List<StoreClass>();
            foreach (var store in temp)
            {
                ret.Add(Map(store));
            }
            return ret;
        }

        public IEnumerable<OrderClass> LoadOrdersByCustomer(CustomerClass customer)
        {
            var ret = PopulateOrderHistory(customer).PreviousOrders;
            return ret;
        }

        public IEnumerable<OrderClass> LoadOrdersByLocation(StoreClass location)
        {
            int locID = _db.Store.Where(s => s.StoreName == location.Name).First().StoreId;
            var dict = GenerateIngrediantDictionary();
            List<PizzaOrder> temp = _db.PizzaOrder
                .Include(po => po.Customer)
                    .ThenInclude(c => c.CustomerAddress)
                .Include(po => po.Store)
                .Include(po => po.PizzasInOrder)
                    .ThenInclude(PiO => PiO.Pizza)
                        .ThenInclude(p => p.IngrediantsOnPizza)
                            .ThenInclude(IoP => IoP.Ingrediant)
                                .ThenInclude(I => I.IngrediantName)
                .Where(o => o.StoreId == locID).ToList();
            List<OrderClass> ret = new List<OrderClass>();
            foreach (var order in temp)
            {
                ret.Add(Map(order, Map(order.Customer), Map(order.Store)));
            }
            return ret;
        }

        public void PlaceOrder(OrderClass order)
        {
            Dictionary<int, string> dict = GenerateIngrediantDictionary();
            _db.Add(Map(order, dict));
            Save();
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
                var orders = _db.PizzaOrder.Where(po => po.StoreId == location.StoreID);
                foreach (var order in orders)
                {
                    order.StoreId = 1;
                }
                _db.Update(orders);
                var adds = _db.CustomerAddress.Where(ca => ca.StoreId == location.StoreID);
                foreach (var add in adds)
                {
                    add.StoreId = 1;
                }
                _db.Update(orders);
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
            _db.Entry(_db.Customer.Find(customer.Username)).CurrentValues.SetValues(Map(customer));
            Save();
        }

        public void UpdateLocation(StoreClass location)
        {
            _db.Entry(_db.Customer.Find(location.Name)).CurrentValues.SetValues(Map(location));
            Save();
        }

        public static Pizza Map(PizzaClass pizzaClass, Dictionary<int, string> IngrediantDictionary)
        {
            Pizza pizza = new Pizza
            {
                Size = (int)pizzaClass.Size,
                Cost = (decimal)pizzaClass.Price,
                PizzaId = pizzaClass.PizzaID
            };

            foreach (var ingrediant in pizzaClass.Ingrediants)
            {
                pizza.IngrediantsOnPizza.Add(new IngrediantsOnPizza { IngrediantId = IngrediantDictionary.FirstOrDefault(i => i.Value == ingrediant).Key, PizzaId = pizzaClass.PizzaID });
            }

            return pizza;
        }
        public static PizzaClass Map(Pizza pizza)
        {
            HashSet<string> ingrediants = new HashSet<string>();
            foreach (var IoP in pizza.IngrediantsOnPizza)
            {
                ingrediants.Add(IoP.Ingrediant.IngrediantName);
            }
            PizzaClass pizzaClass = new PizzaClass((PizzaClass.PizzaSize)pizza.Size, ingrediants)
            {
                PizzaID = pizza.PizzaId
            };

            return pizzaClass;
        }

        internal static Store Map(StoreClass location)
        {
            return new Store
            {
                StoreId = location.StoreID,
                StoreName = location.Name,
                Street = location.Address.Street,
                Street2 = location.Address.Apartment,
                City = location.Address.City,
                Zip = location.Address.Zip,
                State = location.Address.State
            };
        }

        internal static CustomerAddress Map(AddressClass address, int CustomerID)
        {
            return new CustomerAddress
            {
                CustomerAddressId = address.AddressID,
                CustomerId = CustomerID,
                StoreId = address.StoreID,
                Street = address.Street,
                Street2 = address.Apartment,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }

        internal static Customer Map(CustomerClass customer)
        {
            return new Customer
            {
                CustomerId = customer.UserID,
                Username = customer.Username,
                Password = customer.Password,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FavoriteStoreId = customer.FavoriteStoreID
            };
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
            CustomerClass ret = new CustomerClass()
            {
                Username = cust.Username,
                Password = cust.Password,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                UserID = cust.CustomerId,
                FavoriteStoreID = cust.FavoriteStoreId
            };

            List<CustomerAddress> adds = cust.CustomerAddress.ToList();

            foreach (var address in adds)
            {
                ret.Addresses.Add(Map(address));
            }

            return ret;
        }

        internal static StoreClass Map(Store store)
        {
            StoreClass ret = new StoreClass(store.StoreName)
            {
                Address = new AddressClass
                {
                    Street = store.Street,
                    Apartment = store.Street2,
                    City = store.City,
                    Zip = store.Zip,
                    State = store.State
                },
                StoreID = store.StoreId,
                
            };

            ret.Invantory = new Dictionary<string, int>();
            foreach (var Inv in store.Invantory)
            {
                ret.Invantory.Add(Inv.Ingrediant.IngrediantName, Inv.Quantity);
            }

            return ret;
        }

        internal static OrderClass Map(PizzaOrder order, CustomerClass customer, StoreClass store)
        {
            OrderClass ret = new OrderClass
            {
                Store = store,
                Customer = customer,
                DeliveryAddress = Map(order.CustomerAddress),
                OrderID = order.PizzaOrderId,
                DatePlaced = order.DatePlaced,

            };

            foreach (var PiO in order.PizzasInOrder)
            {
                ret.pizzas.Add(Map(PiO.Pizza));
            }

            ret.UpdateTotal();

            return ret;
        }

        private static AddressClass Map(CustomerAddress customerAddress)
        {
            return new AddressClass
            {
                Street = customerAddress.Street,
                Apartment = customerAddress.Street2,
                City = customerAddress.City,
                Zip = customerAddress.Zip,
                State = customerAddress.State,
                AddressID = customerAddress.CustomerAddressId,
                StoreID = customerAddress.StoreId
            };
        }

        internal static PizzaOrder Map(OrderClass order, Dictionary<int, string> IngrediantDictionary)
        {
            PizzaOrder ret = new PizzaOrder
            {
                StoreId = order.Store.StoreID,
                CustomerId = order.Customer.UserID,
                CustomerAddressId = order.DeliveryAddress.AddressID

            };

            return ret;

        }
    }
}
