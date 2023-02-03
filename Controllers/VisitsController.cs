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
    public class VisitsController : Controller
    {
        private readonly proektContext _context;

        public VisitsController(proektContext context)
        {
            _context = context;
        }

        // GET: Visits
        public async Task<IActionResult> Index( string hotelCity)
        {
            IQueryable<string> cityquery = from m in _context.Visit
                                           orderby m.City
                                           select m.City;

            var hotels = from m in _context.Visit
                         select m;

            

            if (!string.IsNullOrEmpty(hotelCity))
            {
                hotels = hotels.Where(x => x.City == hotelCity);
            }


            var CityVM = new CityViewModel
            {
                Cities = new SelectList(await cityquery.Distinct().ToListAsync()),
                Visits = await hotels.ToListAsync()
            };


            return View(CityVM);
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,Address,Description,Picture")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visit);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            return View(visit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Address,Description,Picture")] Visit visit)
        {
            if (id != visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
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
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visit == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visit == null)
            {
                return Problem("Entity set 'proektContext.Visit'  is null.");
            }
            var visit = await _context.Visit.FindAsync(id);
            if (visit != null)
            {
                _context.Visit.Remove(visit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
          return _context.Visit.Any(e => e.Id == id);
        }
    }
}
