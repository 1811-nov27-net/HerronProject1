using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStoreAppLibrary;
using PizzaStoreWeb.Models;

namespace PizzaStoreWeb.Controllers
{
    public class CustomerController : Controller
    {
        public IPizzaStoreRepo Repo { get; }

        public CustomerController(IPizzaStoreRepo repo)
        {
            Repo = repo;
        }

        // GET: Customer
        public ActionResult Index()
        {

            return View(Repo.LoadCustomers().Select(c => Mapper.Map(c)).ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(string username)
        {
            
            return View(Mapper.Map(Repo.LoadCustomerByUsername(username)));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerUI customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(customer);

                Repo.AddCustomer(Mapper.Map(customer));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string username)
        {
            CustomerUI customer = Mapper.Map(Repo.LoadCustomerByUsername(username));
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerUI customer)
        {
            try
            {


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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