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


        public IActionResult Index(string username = "", string action = " ")
        {
            if(username == "")
                return View();
            else if(action == "a")
            {
                return RedirectToAction(nameof(NewAddress));
            }
            else
            {
                return RedirectToAction(nameof(PlaceOrder), new { username });
            }
        }

        //public IActionResult Index(User user)
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Login([Bind("username")] string username = "")
        //{
        //    if (username == "")
        //        return View();
        //    else
        //    {
        //        var user = await _context.User.Include(u => u.Addresses).Include(u => u.Orders).FirstOrDefaultAsync(u => u.Username == username);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }
        //        return RedirectToAction("Index", user);
        //    }
        //}


        // GET: Orders/Create
        public async Task<IActionResult> PlaceOrder(string username)
        {
            try { 
                 var suggestedOrder = await _context.Order.Where(o => o.Username == username).OrderByDescending(o => o.TotalCost).FirstAsync();
                 return View(suggestedOrder);
            }
            catch
            {

            }
            return View(new Order() { Username = username, NumberOfSolidGold = 2 });

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



        // GET: Addresses/Create
        public IActionResult NewAddress()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAddress([Bind("AddressID,Username,StoreName,Street,Zip")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }



        // GET: Users/Create
        public IActionResult NewCustomer()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCustomer([Bind("Username,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

    }
}