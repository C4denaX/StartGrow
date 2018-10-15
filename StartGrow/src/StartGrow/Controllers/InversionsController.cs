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
    public class InversionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InversionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inversions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inversion.Include(i => i.ApplicationUser).Include(i => i.Proyecto).Include(i => i.TipoInversiones);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inversions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversion = await _context.Inversion
                .Include(i => i.ApplicationUser)
                .Include(i => i.Proyecto)
                .Include(i => i.TipoInversiones)
                .SingleOrDefaultAsync(m => m.InversionId == id);
            if (inversion == null)
            {
                return NotFound();
            }

            return View(inversion);
        }

        // GET: Inversions/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre");
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID");
            return View();
        }

        // POST: Inversions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InversionId,ProyectoId,Id,TipoInversionesId,Cuota,Intereses,Total")] Inversion inversion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", inversion.Id);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", inversion.TipoInversionesId);
            return View(inversion);
        }

        // GET: Inversions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversion = await _context.Inversion.SingleOrDefaultAsync(m => m.InversionId == id);
            if (inversion == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", inversion.Id);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", inversion.TipoInversionesId);
            return View(inversion);
        }

        // POST: Inversions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InversionId,ProyectoId,Id,TipoInversionesId,Cuota,Intereses,Total")] Inversion inversion)
        {
            if (id != inversion.InversionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inversion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InversionExists(inversion.InversionId))
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
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", inversion.Id);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", inversion.TipoInversionesId);
            return View(inversion);
        }

        // GET: Inversions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversion = await _context.Inversion
                .Include(i => i.ApplicationUser)
                .Include(i => i.Proyecto)
                .Include(i => i.TipoInversiones)
                .SingleOrDefaultAsync(m => m.InversionId == id);
            if (inversion == null)
            {
                return NotFound();
            }

            return View(inversion);
        }

        // POST: Inversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inversion = await _context.Inversion.SingleOrDefaultAsync(m => m.InversionId == id);
            _context.Inversion.Remove(inversion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InversionExists(int id)
        {
            return _context.Inversion.Any(e => e.InversionId == id);
        }
    }
}
