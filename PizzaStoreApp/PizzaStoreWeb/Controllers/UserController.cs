using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStoreApp;
using PizzaStoreWeb.Models;
using Lib = PizzaStoreAppLibrary;

namespace PizzaStoreWeb.Controllers
{
    public class UserController : Controller
    {
        public Lib.IPizzaStoreRepo Repo { get; }

        public UserController(Lib.IPizzaStoreRepo repo)
        {
            Repo = repo;
        }


        public IActionResult Index()
        {
            try
            {
                CustomerUI user = (CustomerUI) TempData.Peek("user");
                return View(user);
            }
            catch 
            {
                return RedirectToAction(nameof(Login));

            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            try
            {
                CustomerClass user = Mapper.Map((CustomerUI) TempData.Peek("user"));
                if (user == null || user.Username != username)
                    user = Repo.LoadCustomerByUsername(username);
                user.CheckPassword(password);
                TempData["user"] = Mapper.Map(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }


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
                    Repo.AddAddressToCustomer(Mapper.Map(address), Mapper.Map((CustomerUI)TempData.Peek("user")));

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