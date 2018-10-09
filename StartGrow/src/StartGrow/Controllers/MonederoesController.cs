using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartGrow.Data;
using StartGrow.Models;

namespace StartGrow.Controllers
{
    public class MonederoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonederoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Monederoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monedero.ToListAsync());
        }

        // GET: Monederoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedero = await _context.Monedero
                .SingleOrDefaultAsync(m => m.MonederoId == id);
            if (monedero == null)
            {
                return NotFound();
            }

            return View(monedero);
        }

        // GET: Monederoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monederoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonederoId,InversorId,Dinero")] Monedero monedero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monedero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monedero);
        }

        // GET: Monederoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedero = await _context.Monedero.SingleOrDefaultAsync(m => m.MonederoId == id);
            if (monedero == null)
            {
                return NotFound();
            }
            return View(monedero);
        }

        // POST: Monederoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonederoId,InversorId,Dinero")] Monedero monedero)
        {
            if (id != monedero.MonederoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monedero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonederoExists(monedero.MonederoId))
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
            return View(monedero);
        }

        // GET: Monederoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedero = await _context.Monedero
                .SingleOrDefaultAsync(m => m.MonederoId == id);
            if (monedero == null)
            {
                return NotFound();
            }

            return View(monedero);
        }

        // POST: Monederoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monedero = await _context.Monedero.SingleOrDefaultAsync(m => m.MonederoId == id);
            _context.Monedero.Remove(monedero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonederoExists(int id)
        {
            return _context.Monedero.Any(e => e.MonederoId == id);
        }
    }
}
