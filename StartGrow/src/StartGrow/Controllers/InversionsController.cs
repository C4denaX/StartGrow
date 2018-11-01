﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartGrow.Data;
using StartGrow.Models;
using StartGrow.Models.InversionViewModels;

namespace StartGrow.Controllers
{
    //[Authorize (Roles = "Trabajador, Inversor")]
    public class InversionsController : Controller
    {        
        private readonly ApplicationDbContext _context;

        public InversionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET : SELECT
        public IActionResult SelectProyectosForInversion(string[] ids_tiposInversiones)
        {
            SelectProyectosForInversionViewModel selectProyectos = new SelectProyectosForInversionViewModel();

            selectProyectos.Proyectos = _context.Proyecto.Include(p => p.ProyectoAreas).ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).
                Include(p => p.ProyectoTiposInversiones).ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).
                Include(p => p.Rating);

            selectProyectos.Preferencias = _context.Preferencias.Include(p => p.Areas);
            selectProyectos.Preferencias = _context.Preferencias.Include(p => p.Rating);
            selectProyectos.Preferencias = _context.Preferencias.Include(p => p.TiposInversiones);

            selectProyectos.TiposInversiones = _context.TiposInversiones;

            if (ids_tiposInversiones.Length != 0)
            {
                foreach (var i in ids_tiposInversiones)
                {
                    selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => p.ProyectoTiposInversiones.Any(t1 => t1.TiposInversiones.Nombre.Contains(i)));
                }                                
            }

            selectProyectos.Proyectos.ToList();            
            selectProyectos.TiposInversiones.ToList();
            return View(selectProyectos);
        }

        //POST: SELECT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectProyectosForInversion(SelectedProyectosForInversionViewModel selectedProyectos)
        {            
            if (selectedProyectos.IdsToAdd != null)
            {
                return RedirectToAction("Create", selectedProyectos);
            }

            //Se mostrará un mensaje de error al usuario para indicar que seleccione algún proyecto
            ModelState.AddModelError(string.Empty, "Debes seleccionar al menos un proyecto");

            //Se recreará otra vez el view model
            SelectProyectosForInversionViewModel selectProyectos = new SelectProyectosForInversionViewModel();

            //selectProyectos.Proyectos = new SelectList(_context.Proyecto.Select<Proyecto>(g => g.Nombre).ToList());

            selectProyectos.Proyectos = _context.Proyecto.Include(m => m.Nombre).ToList();
            return View(selectProyectos);
        }

        // GET: Inversions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inversion.Include(i => i.Inversor).Include(i => i.Proyecto).Include(i => i.TipoInversiones);
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
                .Include(i => i.Inversor)
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre");
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesId", "Nombre");
            return View();
        }

        // POST: Inversions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InversionId,ProyectoId,ApplicationUserId,TipoInversionesId,Cuota,Intereses,Total")] Inversion inversion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InversorId"] = new SelectList(_context.Users, "Id", "Id", inversion.InversorId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesId", "Nombre", inversion.TipoInversionesId);
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
            ViewData["InversorId"] = new SelectList(_context.Users, "Id", "Id", inversion.InversorId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesId", "Nombre", inversion.TipoInversionesId);
            return View(inversion);
        }

        // POST: Inversions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InversionId,ProyectoId,ApplicationUserId,TipoInversionesId,Cuota,Intereses,Total")] Inversion inversion)
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
            ViewData["InversorId"] = new SelectList(_context.Users, "Id", "Id", inversion.InversorId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyecto, "ProyectoId", "Nombre", inversion.ProyectoId);
            ViewData["TipoInversionesId"] = new SelectList(_context.TiposInversiones, "TiposInversionesId", "Nombre", inversion.TipoInversionesId);
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
                .Include(i => i.Inversor)
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
