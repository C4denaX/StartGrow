using System;
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
    [Authorize (Roles = "Trabajador, Inversor")]
    public class InversionsController : Controller
    {        
        private readonly ApplicationDbContext _context;

        public InversionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET : SELECT
        public IActionResult SelectProyectosForInversion(string[] ids_tiposInversiones, string[] ids_areas, string[] ids_rating, int? invMin, float? interes, int? plazo)
        {
            SelectProyectosForInversionViewModel selectProyectos = new SelectProyectosForInversionViewModel();

            selectProyectos.TiposInversiones = _context.TiposInversiones;
            selectProyectos.Areas = _context.Areas;
            selectProyectos.Rating = _context.Rating;

            selectProyectos.Proyectos = _context.Proyecto.Include(p => p.Rating).Include(p => p.ProyectoAreas).
                ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).Include(p => p.ProyectoTiposInversiones).
                ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).Where(p => p.Plazo != null).Where(p => p.RatingId != null);
            
            if (ids_tiposInversiones.Length != 0)
            {
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => p.ProyectoTiposInversiones.Any(t1 => ids_tiposInversiones.Contains(t1.TiposInversiones.Nombre)));                                                
            }
            
            if (ids_areas.Length != 0)
            {
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => p.ProyectoAreas.Any(t1 => ids_areas.Contains(t1.Areas.Nombre)));
            }
            
            if (ids_rating.Length != 0)
            {
                //selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => p.Rating.Any(t1 => ids_rating.Contains(t1.Rating.Nombre)));          
            }

            if (invMin != null)
            {
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => p.MinInversion.CompareTo((float)invMin) >= 0);
            }
            
            if (interes != null)
            {
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => ((float)p.Interes).CompareTo(interes) >= 0);
            }

            if (plazo != null)
            {
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => ((int) p.Plazo).CompareTo((int) plazo) >= 0);
            }
            
            selectProyectos.Proyectos.ToList();                        
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
            ModelState.AddModelError(string.Empty, "Debes seleccionar al menos un proyecto para invertir");

            //Se recreará otra vez el View Model
            SelectProyectosForInversionViewModel selectProyectos = new SelectProyectosForInversionViewModel();

            selectProyectos.TiposInversiones = _context.TiposInversiones;
            selectProyectos.Areas = _context.Areas;
            selectProyectos.Rating = _context.Rating;

            selectProyectos.Proyectos = _context.Proyecto.Include(p => p.Rating).Include(p => p.ProyectoAreas).
                ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).Include(p => p.ProyectoTiposInversiones).
                ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).Where(p => p.Plazo != null).Where(p => p.RatingId != null);
            
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

        // GET: Inversions/CREATE
        public IActionResult Create(SelectedProyectosForInversionViewModel selectedProyectos)
        {
            Proyecto proyecto;
            int id;
                        
            InversionesCreateViewModel inversion = new InversionesCreateViewModel();
            inversion.inversiones = new List<InversionCreateViewModel>();

            Inversor inversor = _context.Users.OfType<Inversor>().FirstOrDefault<Inversor>(p => p.UserName.Equals(User.Identity.Name));
            inversion.Name = inversor.Nombre;
            inversion.FirstSurname = inversor.Apellido1;
            inversion.SecondSurname = inversor.Apellido2;

            if (selectedProyectos.IdsToAdd == null || selectedProyectos.IdsToAdd.Count() == 0)
            {
                return RedirectToAction("SelectProyectosForInversion");                
            }

            else
            {
                foreach (string ids in selectedProyectos.IdsToAdd)
                {
                    id = int.Parse(ids);
                    proyecto = _context.Proyecto.Include(p => p.Rating).Include(p => p.ProyectoAreas).
                              ThenInclude<Proyecto, ProyectoAreas, Areas>(p => p.Areas).Include(p => p.ProyectoTiposInversiones).
                              ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).Where(p => p.Plazo != null).Where(p => p.RatingId != null).
                              FirstOrDefault<Proyecto>(p => p.ProyectoId.Equals(id));
                    inversion.inversiones.Add(new InversionCreateViewModel()
                    {
                        Cuota = 0,
                        inversion = new Inversion()
                        {Proyecto = proyecto, Inversor = inversor}
                    });
                }
            }

            ViewBag.Rating = new SelectList(_context.Rating.Select(c => c.Nombre).Distinct());
            ViewBag.TiposInversiones = new SelectList(Enum.GetNames(typeof(StartGrow.Models.Estados)));
            ViewBag.Inversor = inversor;

            return View(inversion);   
        }

        // POST: Inversions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InversionesCreateViewModel inversionCreate)
        {
            var inversiones = new List<InversionCreateViewModel>();
            Proyecto proyecto;
            Inversion inversion;
            Inversor inversor = _context.Users.OfType<Inversor>().FirstOrDefault<Inversor>(p => p.UserName.Equals(User.Identity.Name));

            List<int> idsInversion = new List<int>();
            ModelState.Clear();
            
            foreach (InversionCreateViewModel item in inversionCreate.inversiones)
            {
                proyecto = await _context.Proyecto.Include(p => p.ProyectoTiposInversiones).
                           ThenInclude<Proyecto, ProyectoTiposInversiones, TiposInversiones>(p => p.TiposInversiones).
                           FirstOrDefaultAsync<Proyecto>(m => m.ProyectoId == item.inversion.Proyecto.ProyectoId);

                inversion = await _context.Inversion.FirstOrDefaultAsync<Inversion>(p => p.InversionId == item.inversion.InversionId);

                float MinInv = proyecto.MinInversion;
                if (item.tipoinversion.Nombre == null && item.Cuota.CompareTo(MinInv) <= 0)
                {
                    ModelState.AddModelError("Inversión incorrecta", "Por favor, seleccione una inversión e introduzca una cantidad válida");
                }
                else if (item.tipoinversion.Nombre == null && item.Cuota.CompareTo(MinInv) >= 0)
                {
                    ModelState.AddModelError("Tipo de Inversión No seleccionada", "Por favor, seleccione un tipo de inversión");
                }
                else if (item.tipoinversion.Nombre != null && item.Cuota.CompareTo(MinInv) <= 0)
                {
                    ModelState.AddModelError("Cuota incorrecta", "Por favor, introduzca una couta válida.");
                }
                else
                {
                    inversion.Cuota = item.Cuota;
                    
                    //Actualizamos Monedero
                    inversor = await _context.Users.OfType<Inversor>().Include(m => m.Monedero).FirstOrDefaultAsync<Inversor>(c => c.UserName.Equals(User.Identity.Name));

                    
                }
            }

            

            return RedirectToAction("Details", idsInversion);


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