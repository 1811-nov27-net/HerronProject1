using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVPizza.DataAccess;

namespace MVPizza.Controllers
{
    public class AdminController : Controller
    {
        private readonly MVPizzaDBContext _context;

        public AdminController(MVPizzaDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}