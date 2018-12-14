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
    public class StoreController : Controller
    {
        public IPizzaStoreRepo Repo { get; set; }

        public StoreController(IPizzaStoreRepo repo)
        {
            Repo = repo;
        }
        
        
        
        // GET: Store
        public ActionResult Index()
        {
            return View(Repo.LoadLocations().Select());
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreUI store)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
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