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
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Proyecto.Include(p => p.ProyectoAreas).Include(p => p.Rating);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .Include(p => p.ProyectoAreas)
                .Include(p => p.Rating)
                .SingleOrDefaultAsync(m => m.ProyectoId == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            ViewData["AreasID"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID");
            ViewData["RatingID"] = new SelectList(_context.Rating, "RatingID", "RatingID");
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProyectoId,AreasID,RatingID,Nombre,MinInversion,Plazo,Interes,Importe,Progreso,NumInversores,FechaExpiracion")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreasID"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", proyecto.ProyectoAreas);
            ViewData["RatingID"] = new SelectList(_context.Rating, "RatingID", "RatingID", proyecto.RatingId);
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto.SingleOrDefaultAsync(m => m.ProyectoId == id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["AreasID"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", proyecto.ProyectoAreas);
            ViewData["RatingID"] = new SelectList(_context.Rating, "RatingID", "RatingID", proyecto.RatingId);
            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoId,AreasID,RatingID,Nombre,MinInversion,Plazo,Interes,Importe,Progreso,NumInversores,FechaExpiracion")] Proyecto proyecto)
        {
            if (id != proyecto.ProyectoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.ProyectoId))
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
            ViewData["AreasID"] = new SelectList(_context.TiposInversiones, "TiposInversionesID", "TiposInversionesID", proyecto.ProyectoAreas);
            ViewData["RatingID"] = new SelectList(_context.Rating, "RatingID", "RatingID", proyecto.RatingId);
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .Include(p => p.ProyectoAreas)
                .Include(p => p.Rating)
                .SingleOrDefaultAsync(m => m.ProyectoId == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyecto.SingleOrDefaultAsync(m => m.ProyectoId == id);
            _context.Proyecto.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyecto.Any(e => e.ProyectoId == id);
        }
    }
}
