using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartGrow.Data;
using StartGrow.Models;
using StartGrow.Models.InversionRecuperadaViewModels;

namespace StartGrow.Controllers
{
    public class InversionRecuperadasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InversionRecuperadasController(ApplicationDbContext context)
        {
            _context = context;
        }






        //SELECT (GET)
        public IActionResult SelectInversionForRecuperarInversion(string nombreInversion, string sectorSeleccionado,
            string estadoSeleccionado, string tipoSeleccionado, string ratingSeleccionado)
        {
            //Creamos un objeto de tipo SelectInversionForRecuperarInversionViewModel usado para renderizar la vista SelectInversionForRecuperarInversion.
            SelectInversionForRecuperarInversionViewModel selectInversiones = new SelectInversionForRecuperarInversionViewModel();



            //******* FILTRAR POR RATING *******
       
            //Para que el desplegable ofrezca la lista de Ratings que hay en la BD.
            selectInversiones.Ratings = new SelectList(_context.Rating.Select(r => r.Nombre).ToList());

            //Utilizado si el usuario selecciona un género en el desplegable. Al seleccionar dicho género, 
            //se añadirá al IEnumerable Inversiones todas las inversiones donde el rating sea el rating seleccionado.
            if (ratingSeleccionado != null)
            {
                selectInversiones.Inversiones = selectInversiones.Inversiones.Where(i => i.)
            }










            selectInversiones.Inversiones = _context.Inversion.Include(m => m.TipoInversiones).Include(m => m.Proyecto).
                ThenInclude<Inversion, Proyecto, Rating>(p => p.Rating).Where(m => m.TipoInversiones.Equals(tipoSeleccionado));

            //Utilizado si el usuario quiere buscar por título.
            if (nombreInversion != null)
                selectInversiones.Inversiones = selectInversiones.Inversiones.Where(m => m.InversionId.Equals(nombreInversion));
          
            selectInversiones.Inversiones.ToList();
            return View(selectInversiones);
        }







            // GET: InversionRecuperadas
            public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InversionRecuperada.Include(i => i.Inversion).Include(i => i.Monedero);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InversionRecuperadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversionRecuperada = await _context.InversionRecuperada
                .Include(i => i.Inversion)
                .Include(i => i.Monedero)
                .SingleOrDefaultAsync(m => m.InversionRecuperadaId == id);
            if (inversionRecuperada == null)
            {
                return NotFound();
            }

            return View(inversionRecuperada);
        }

        // GET: InversionRecuperadas/Create
        public IActionResult Create()
        {
            ViewData["InversionId"] = new SelectList(_context.Inversion, "InversionId", "Id");
            ViewData["MonederoId"] = new SelectList(_context.Monedero, "MonederoId", "Id");
            return View();
        }

        // POST: InversionRecuperadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InversionRecuperadaId,FechaRecuperacion,Comentario,CantidadRecuperada,MonederoId,InversionId")] InversionRecuperada inversionRecuperada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inversionRecuperada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InversionId"] = new SelectList(_context.Inversion, "InversionId", "Id", inversionRecuperada.InversionId);
            ViewData["MonederoId"] = new SelectList(_context.Monedero, "MonederoId", "Id", inversionRecuperada.MonederoId);
            return View(inversionRecuperada);
        }

        // GET: InversionRecuperadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversionRecuperada = await _context.InversionRecuperada.SingleOrDefaultAsync(m => m.InversionRecuperadaId == id);
            if (inversionRecuperada == null)
            {
                return NotFound();
            }
            ViewData["InversionId"] = new SelectList(_context.Inversion, "InversionId", "Id", inversionRecuperada.InversionId);
            ViewData["MonederoId"] = new SelectList(_context.Monedero, "MonederoId", "Id", inversionRecuperada.MonederoId);
            return View(inversionRecuperada);
        }

        // POST: InversionRecuperadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InversionRecuperadaId,FechaRecuperacion,Comentario,CantidadRecuperada,MonederoId,InversionId")] InversionRecuperada inversionRecuperada)
        {
            if (id != inversionRecuperada.InversionRecuperadaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inversionRecuperada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InversionRecuperadaExists(inversionRecuperada.InversionRecuperadaId))
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
            ViewData["InversionId"] = new SelectList(_context.Inversion, "InversionId", "Id", inversionRecuperada.InversionId);
            ViewData["MonederoId"] = new SelectList(_context.Monedero, "MonederoId", "Id", inversionRecuperada.MonederoId);
            return View(inversionRecuperada);
        }

        // GET: InversionRecuperadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inversionRecuperada = await _context.InversionRecuperada
                .Include(i => i.Inversion)
                .Include(i => i.Monedero)
                .SingleOrDefaultAsync(m => m.InversionRecuperadaId == id);
            if (inversionRecuperada == null)
            {
                return NotFound();
            }

            return View(inversionRecuperada);
        }

        // POST: InversionRecuperadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inversionRecuperada = await _context.InversionRecuperada.SingleOrDefaultAsync(m => m.InversionRecuperadaId == id);
            _context.InversionRecuperada.Remove(inversionRecuperada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InversionRecuperadaExists(int id)
        {
            return _context.InversionRecuperada.Any(e => e.InversionRecuperadaId == id);
        }
    }
}
