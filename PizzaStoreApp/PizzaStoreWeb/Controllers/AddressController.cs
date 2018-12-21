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
    public class AddressController : Controller
    {
        public IPizzaStoreRepo Repo { get; }

        public AddressController(IPizzaStoreRepo repo)
        {
            Repo = repo;
        }

        // GET: Address
        public ActionResult Index(string username = "")
        {
            if(username == "")
                return View();
            else
            {
                List<AddressUI> adds = Repo.LoadCustomerByUsername(username).Addresses.Select(a => Mapper.Map(a)).ToList();
                return View(adds);
            }
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            AddressUI add = Mapper.Map(Repo.LoadAddressByID(id));
            return View(add);
        }
        
        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressUI address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception();
                }

                Repo.UpdateAddress(Mapper.Map(address));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}