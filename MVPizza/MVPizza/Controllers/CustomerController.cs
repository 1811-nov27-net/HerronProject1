using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public async Task<IActionResult> Index(string ans = "", string username = "")
        {
            if(username == "")
                return View();
            else if(ans == "a")
            {
                try
                {
                    User User = await _context.User.FirstAsync(u => u.Username == username); // check to see if username exists
                    return RedirectToAction(nameof(NewAddress), new { username = User.Username });

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
                    User User = await _context.User.FirstAsync(u => u.Username == username); // check to see if username exists
                    return RedirectToAction(nameof(PlaceOrder), new { username = User.Username });

                }
                catch
                {
                    return View();
                }
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
                suggestedOrder.PossibleAddresses = await _context.Address.Where(a => a.Username == username).ToListAsync();
                return View(suggestedOrder);
            }
            catch
            {

            }
            var possAdd = await _context.Address.Where(a => a.Username == username).ToListAsync();
            return View(new Order() { Username = username, NumberOfSolidGold = 2, PossibleAddresses = possAdd });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder([Bind("OrderID,Username,AddressID,NumberOfSupremes,NumberOfMeatLovers,NumberOfVeggie,NumberOfSolidGold")] Order order)
        {
            DateTime lastTime = new DateTime();
            try
            {
                lastTime = (await _context.Order.Where(o => o.Username == order.Username).Where(o => o.AddressID == order.AddressID).OrderBy(o => o.TimePlaced).FirstAsync()).TimePlaced;

            }
            catch
            {
                lastTime = DateTime.Now.AddDays(-1);
                
            }
            var deliveryAddress = await _context.Address.FirstAsync(a => a.AddressID == order.AddressID);
            order.Store = await _context.Store.FirstAsync(s => s.StoreName == deliveryAddress.StoreName); // assume one store per zip, stores deliver anywhere in their zip
            order.StoreName = deliveryAddress.StoreName; // obviously
            order.User = await _context.User.FirstAsync(u => u.Username == order.Username);
            if (ModelState.IsValid && order.VerifyOrder(lastTime))
            {
                _context.Add(order);
                var store = await _context.Store.FirstOrDefaultAsync(s => s.StoreName == order.StoreName);
                store.NumberOfVeggie -= order.NumberOfVeggie;
                store.NumberOfSupremes -= order.NumberOfSupremes;
                store.NumberOfMeatLovers -= order.NumberOfMeatLovers;
                store.NumberOfSolidGold -= order.NumberOfSolidGold;
                _context.Store.Update(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var possAdd = await _context.Address.Where(a => a.Username == order.Username).ToListAsync();
            order.PossibleAddresses = possAdd;
            return View(order);
        }



        // GET: Addresses/Create
        public async Task<IActionResult> NewAddress(string username)
        {
            List<SelectListItem> ZipSelect = new List<SelectListItem>();
            List<int> possZips = await _context.Store.Select(s => s.Zip).ToListAsync();
            foreach (var zip in possZips)
            {
                ZipSelect.Add(new SelectListItem(zip.ToString(), zip.ToString()));
            }
            ViewData["Zip"] = ZipSelect;
            return View(new Address() {Username = username });
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAddress([Bind("Username,Street,Zip")] Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    address.StoreName = (await _context.Store.FirstAsync(s => s.Zip == address.Zip)).StoreName;

                }
                catch (Exception)
                {
                    return View(address);
                    throw;
                }
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