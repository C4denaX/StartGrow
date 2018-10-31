using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartGrow.Data;
using StartGrow.Models;
using StartGrow.Models.SolicitudViewModels;

namespace StartGrow.Controllers
{
    public class SolicitudesController : Controller 
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Solicitudes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Solicitud.Include(s => s.Proyecto).Include(s => s.Trabajador);
            return View(await applicationDbContext.ToListAsync());
        }


        //GET: Select
        public IActionResult SelectProyectosForSolicitud(string nombreProyecto, string[] tipoSeleccionado, string areasSeleccionada, int? capital, DateTime? fecha
    )
        {
            SelectProyectosForSolicitudViewModel selectProyecto = new SelectProyectosForSolicitudViewModel();
            selectProyecto.areas = new SelectList(_context.Areas.Select(a =>a.Nombre).ToList());
            selectProyecto.Tipos = _context.TiposInversiones;
            selectProyecto.proyectos = _context.Proyecto.Include(m => m.Rating).Include(m => m.ProyectoAreas).
                ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).Include(p => p.ProyectoTiposInversiones).
                ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).Where(p=> p.RatingId == null);
            
            if (nombreProyecto != null)
            {
                selectProyecto.proyectos = selectProyecto.proyectos.Where(p => p.Nombre.Contains(nombreProyecto));
            }

            if (capital != null)
            {
                
                selectProyecto.proyectos = selectProyecto.proyectos.Where(p => p.Importe.CompareTo((float)capital) >= 0);
            }

            if(tipoSeleccionado.Length !=0)
            {
                foreach(String i in tipoSeleccionado){
                    selectProyecto.proyectos = selectProyecto.proyectos.Where(p => p.ProyectoTiposInversiones.Any(ti => ti.TiposInversiones.Nombre.Contains(i)));
                }
            }

            if(areasSeleccionada != null)
            {
                selectProyecto.proyectos = selectProyecto.proyectos.Where(p => p.ProyectoAreas.Any(a => a.Areas.Nombre.Contains(areasSeleccionada)));
            }

            if(fecha != null)
            {
                selectProyecto.proyectos = selectProyecto.proyectos.Where(p => p.FechaExpiracion.Date.Equals(fecha));
            }
            selectProyecto.proyectos.ToList();

            return View(selectProyecto);
        }

        //POST: Select 
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectProyectosForSolicitud(SelectedProyectosForSolicitudViewModel selectedproyectos)
        {
            if (selectedproyectos.IdsToAdd != null)
            {
                return RedirectToAction("Create", selectedproyectos);
            }


            ModelState.AddModelError(string.Empty, "Debes seleccionar al menos 1 proyecto para publicar");

            SelectProyectosForSolicitudViewModel selectProyecto = new SelectProyectosForSolicitudViewModel();
            selectProyecto.areas = new SelectList(_context.Areas.Select(a => a.Nombre));
            selectProyecto.Tipos = _context.TiposInversiones;
            selectProyecto.proyectos = _context.Proyecto.Include(m => m.Rating).Include(m => m.ProyectoAreas).
                           ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).Include(p => p.ProyectoTiposInversiones).
                           ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).Where(m => m.RatingId == null);
            return View(selectProyecto);
        }
        // GET: Solicitudes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Proyecto)
                .Include(s => s.Trabajador)
                .SingleOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicitudes/Create
        public IActionResult Create()
        {
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre");
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajador, "Id", "Id");
            return View();
        }

        // POST: Solicitudes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitudId,TrabajadorId,ProyectoId,Estado,FechaSolicitud")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", solicitud.ProyectoId);
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajador, "Id", "Id", solicitud.TrabajadorId);
            return View(solicitud);
        }

        // GET: Solicitudes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud.SingleOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", solicitud.ProyectoId);
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajador, "Id", "Id", solicitud.TrabajadorId);
            return View(solicitud);
        }

        // POST: Solicitudes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudId,TrabajadorId,ProyectoId,Estado,FechaSolicitud")] Solicitud solicitud)
        {
            if (id != solicitud.SolicitudId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.SolicitudId))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", solicitud.ProyectoId);
            ViewData["TrabajadorId"] = new SelectList(_context.Trabajador, "Id", "Id", solicitud.TrabajadorId);
            return View(solicitud);
        }

        // GET: Solicitudes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Proyecto)
                .Include(s => s.Trabajador)
                .SingleOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = await _context.Solicitud.SingleOrDefaultAsync(m => m.SolicitudId == id);
            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(int id)
        {
            return _context.Solicitud.Any(e => e.SolicitudId == id);
        }
    }
}
