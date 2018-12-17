using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib = PizzaStoreAppLibrary;

namespace PizzaStoreWeb.Controllers
{
    public class AdminController : Controller
    {
        public Lib.IPizzaStoreRepo Repo { get; }

        public AdminController(Lib.IPizzaStoreRepo repo)
        {
            Repo = repo;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}