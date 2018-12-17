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
    public class OrdersController : Controller
    {
        private readonly MVPizzaDBContext _context;

        public OrdersController(MVPizzaDBContext context)
        {
            _context = context;
        }

        // GET: Orders
        // type: n for no query, s for store id, a for address id, u (or anything else) for user id
        // char: d for date, i for date inverted, c for cheapest, e for expensive
        public async Task<IActionResult> Index(char type = 'n', char order = 'd', string search = "")
        {
            
            if (type == 'n')
                return View(await _context.Order.ToListAsync());
            else if(type == 's')
            {
                if(order == 'd')
                    return View(await _context.Order.Where(o => o.StoreName == search).OrderBy(o => o.TimePlaced).ToListAsync());
                else if(order == 'i')
                    return View(await _context.Order.Where(o => o.StoreName == search).OrderByDescending(o => o.TimePlaced).ToListAsync());
                else if (order == 'c')
                    return View(await _context.Order.Where(o => o.StoreName == search).OrderBy(o => o.TotalCost).ToListAsync());
                else
                    return View(await _context.Order.Where(o => o.StoreName == search).OrderByDescending(o => o.TotalCost).ToListAsync());


            }
            else if (type == 'a')
            {
                if (order == 'd')
                    return View(await _context.Order.Where(o => o.AddressID == int.Parse(search)).OrderBy(o => o.TimePlaced).ToListAsync());
                else if (order == 'i')
                    return View(await _context.Order.Where(o => o.AddressID == int.Parse(search)).OrderByDescending(o => o.TimePlaced).ToListAsync());
                else if (order == 'c')
                    return View(await _context.Order.Where(o => o.AddressID == int.Parse(search)).OrderBy(o => o.TotalCost).ToListAsync());
                else
                    return View(await _context.Order.Where(o => o.AddressID == int.Parse(search)).OrderByDescending(o => o.TotalCost).ToListAsync());

            }
            else
            {
                if (order == 'd')
                    return View(await _context.Order.Where(o => o.Username == search).OrderBy(o => o.TimePlaced).ToListAsync());
                else if (order == 'i')
                    return View(await _context.Order.Where(o => o.Username == search).OrderByDescending(o => o.TimePlaced).ToListAsync());
                else if (order == 'c')
                    return View(await _context.Order.Where(o => o.Username == search).OrderBy(o => o.TotalCost).ToListAsync());
                else
                    return View(await _context.Order.Where(o => o.Username == search).OrderByDescending(o => o.TotalCost).ToListAsync());

            }

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,Username,StoreName,AddressID,TimePlaced,NumberOfSupremes,NumberOfMeatLovers,NumberOfVeggie,NumberOfSolidGold")] Order order)
        {
            if (ModelState.IsValid)
            {


                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,Username,StoreName,AddressID,TimePlaced,NumberOfSupremes,NumberOfMeatLovers,NumberOfVeggie,NumberOfSolidGold,TotalCost")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
