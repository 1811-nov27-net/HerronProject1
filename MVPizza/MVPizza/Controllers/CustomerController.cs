using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPizza.DataAccess;

namespace MVPizza.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MVPizzaDBContext _context;

        public CustomerController(MVPizzaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Orders/Create
        public IActionResult PlaceOrder()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder([Bind("OrderID,Username,StoreName,AddressID,NumberOfSupremes,NumberOfMeatLovers,NumberOfVeggie,NumberOfSolidGold")] Order order)
        {
            var lastTime = await _context.Order.Where(o => o.Username == order.Username).OrderBy(o => o.TimePlaced).FirstAsync();
            if (ModelState.IsValid && order.VerifyOrder(lastTime.TimePlaced))
            {


                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

    }
}