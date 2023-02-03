using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proekt.Data;
using proekt.Models;
using proekt.ViewModels;

namespace proekt.Controllers
{
    public class GuestHousesController : Controller
    {
        private readonly proektContext _context;

        public GuestHousesController(proektContext context)
        {
            _context = context;
        }

        // GET: GuestHouses
        public async Task<IActionResult> Index(string searchString, string hotelCity)
        {
            IQueryable<string> cityquery = from m in _context.GuestHouse
                                           orderby m.City
                                           select m.City;

            var guesthouses = from m in _context.GuestHouse
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                guesthouses = guesthouses.Where(s => s.Name!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(hotelCity))
            {
                guesthouses = guesthouses.Where(x => x.City == hotelCity);
            }


            var CityVM = new CityViewModel
            {
                Cities = new SelectList(await cityquery.Distinct().ToListAsync()),
                GuestHouses= await guesthouses.ToListAsync()
            };


            return View(CityVM);
        }

        // GET: GuestHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GuestHouse == null)
            {
                return NotFound();
            }

            var guestHouse = await _context.GuestHouse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestHouse == null)
            {
                return NotFound();
            }

            return View(guestHouse);
        }

        // GET: GuestHouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,Name,City,Address,Phone,Description,Picture")] GuestHouse guestHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestHouse);
        }

        // GET: GuestHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GuestHouse == null)
            {
                return NotFound();
            }

            var guestHouse = await _context.GuestHouse.FindAsync(id);
            if (guestHouse == null)
            {
                return NotFound();
            }
            return View(guestHouse);
        }

        // POST: GuestHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Name,City,Address,Phone,Description,Picture")] GuestHouse guestHouse)
        {
            if (id != guestHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestHouseExists(guestHouse.Id))
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
            return View(guestHouse);
        }

        // GET: GuestHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GuestHouse == null)
            {
                return NotFound();
            }

            var guestHouse = await _context.GuestHouse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestHouse == null)
            {
                return NotFound();
            }

            return View(guestHouse);
        }

        // POST: GuestHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GuestHouse == null)
            {
                return Problem("Entity set 'proektContext.GuestHouse'  is null.");
            }
            var guestHouse = await _context.GuestHouse.FindAsync(id);
            if (guestHouse != null)
            {
                _context.GuestHouse.Remove(guestHouse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestHouseExists(int id)
        {
          return _context.GuestHouse.Any(e => e.Id == id);
        }
    }
}
