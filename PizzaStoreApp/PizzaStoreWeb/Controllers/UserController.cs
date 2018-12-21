using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStoreApp;
using PizzaStoreAppLibrary;
using PizzaStoreWeb.Models;

namespace PizzaStoreWeb.Controllers
{
    public class UserController : Controller
    {
        public IPizzaStoreRepo Repo { get; }

        public UserController(IPizzaStoreRepo repo)
        {
            Repo = repo;
        }



        public ActionResult Index(string ans = "", string username = "")
        {
            if (username == "")
                return View();
            else if (ans == "a")
            {
                try
                {
                    CustomerClass User = Repo.LoadCustomerByUsername(username); // check to see if username exists

                    return RedirectToAction(nameof(AddAddress), new { username = User.Username });

                }
                catch
                {

                    return View();
                }
            }
            else
            {
                try
                {
                    CustomerClass User = Repo.LoadCustomerByUsername(username); // check to see if username exists
                    TempData["user"] = username;
                    return RedirectToAction(nameof(PlaceOrder));

                }
                catch
                {
                    return View();
                }
            }
        }

        private ActionResult PlaceOrder()
        {
            CustomerClass User = Repo.LoadCustomerByUsername((string)TempData["user"]);
            User.PreviousOrders = (List<OrderClass>)Repo.LoadOrdersByCustomer(User);
            CustomerUI customer = Mapper.Map(User);
            if (customer.SuggestedOrder == null)
            {
                OrderUI order = new OrderUI();
                order.Customer = customer;
                return View(order);
            }
            List<int> RecentStoreZips = User.PreviousOrders.Where(po => po.DatePlaced.Subtract(DateTime.Now) < TimeSpan.FromHours(2)).Select(po => po.Store.Address.Zip).ToList();
            ViewData["addresses"] = customer.Addresses.Where(a => !RecentStoreZips.Contains(a.Zip));
            return View(customer.SuggestedOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(OrderUI order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrderClass TheOrder = Mapper.Map(order);
                    TheOrder.VerifyOrder();
                    Repo.PlaceOrder(TheOrder);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View(order);
            }

        }

        //public IActionResult Login()
        //{
        //    return View();
        //}

        //public IActionResult Login(string username, string password)
        //{
        //    try
        //    {
        //        CustomerClass user = Repo.LoadCustomerByUsername(username);
        //        Repo.CheckPassword(user, password);
        //        TempData["user"] = username;
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {

        //        return View();
        //    }
        //}


        // GET: Address/Create


        


        public ActionResult AddAddress()
        {
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddress(AddressUI address)
        {
            try
            {
                if (ModelState.IsValid)
                    Repo.AddAddressToCustomer(Mapper.Map(address), (CustomerClass)TempData.Peek("user"));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Address/Edit/5
        public ActionResult EditAddress(int id)
        {
            return View();
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddress(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Address/Delete/5
        public ActionResult DeleteAddress(int id)
        {
            return View();
        }

        // POST: Address/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddress(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}