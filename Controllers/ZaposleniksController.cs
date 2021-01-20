using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zaposlenici.Models;
using Microsoft.AspNetCore.Authorization;

namespace Zaposlenici.Controllers
{
    public class ZaposleniksController : Controller
    {
        private readonly ZaposlenikContext _context;

        public ZaposleniksController(ZaposlenikContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Zaposleniks
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var zapl = from zp in _context.zaposlenik
                       select zp;
            if (!String.IsNullOrEmpty(searchString))
            {
                zapl = zapl.Where(zp => zp.Ime.Contains(searchString));
            }
            return View(await zapl.AsNoTracking().ToListAsync());
        }

        [Authorize]
        // GET: Zaposleniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // GET: Zaposleniks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zaposleniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Datum_zaposl")] Zaposlenik zaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaposlenik);
        }

        // GET: Zaposleniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            if (zaposlenik == null)
            {
                return NotFound();
            }
            return View(zaposlenik);
        }

        // POST: Zaposleniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Datum_zaposl")] Zaposlenik zaposlenik)
        {
            if (id != zaposlenik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenikExists(zaposlenik.Id))
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
            return View(zaposlenik);
        }

        // GET: Zaposleniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // POST: Zaposleniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            _context.Zaposlenik.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenikExists(int id)
        {
            return _context.Zaposlenik.Any(e => e.Id == id);
        }
    }
}
