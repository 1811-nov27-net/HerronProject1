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
        public IPizzaStoreRepo Repo { get; }

        public StoreController(IPizzaStoreRepo repo)
        {
            Repo = repo;
        }

        // GET: Store
        public ActionResult Index()
        {
            List<StoreClass> storeClasses = Repo.LoadLocations().ToList();
            List<StoreUI> storeUIs = new List<StoreUI>();
            foreach (var store in storeClasses)
            {
                storeUIs.Add(Mapper.Map(store));
            }
            return View(storeUIs);
        }

        // GET: Store/Details/5
        public ActionResult Details(int StoreId)
        {
            
            return View(Mapper.Map(Repo.LoadLocationByID(StoreId)));
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
                if (!ModelState.IsValid)
                    throw new Exception();
                Repo.AddStore(Mapper.Map(store));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int StoreId)
        {
            StoreUI store = Mapper.Map(Repo.LoadLocationByID(StoreId));
            return View(store);
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreUI store)
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