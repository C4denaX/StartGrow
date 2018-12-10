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
    [Authorize(Roles = "Inversor")]
    public class InversionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InversionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET : Inversions/SELECT
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
                selectProyectos.Proyectos = selectProyectos.Proyectos.Where(p => ((int)p.Plazo).CompareTo((int)plazo) >= 0);
            }

            selectProyectos.Proyectos.ToList();
            return View(selectProyectos);
        }

        //POST: Inversions/SELECT
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
                        Cuota = 0, Interes = (float)proyecto.Interes, NombreProyecto = proyecto.Nombre, MinInver = proyecto.MinInversion,
                        TiposInversion = new SelectList(proyecto.ProyectoTiposInversiones.Select(pro => pro.TiposInversiones.Nombre).ToList()),
                        Rating = proyecto.Rating.Nombre, Plazo = (int)proyecto.Plazo,

                        inversion = new Inversion()
                        {Proyecto = proyecto}
                        
                    });

                }
            }

            ViewBag.Cuota = new SelectList(_context.Inversion.Select(c => c.Cuota).Distinct());
          //  ViewBag.TiposInversiones = new SelectList(_context.ProyectoTiposInversiones.Select(c => c.TiposInversiones.Nombre).Distinct());
            ViewBag.Inversor = inversor;

            return View(inversion);
        }

        // POST: Inversions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InversionesCreateViewModel inversionCreate, IList<InversionCreateViewModel> inversiones)
        {
            //var inversiones = new List<InversionCreateViewModel>();
            Inversion inversion;
            Proyecto proyecto;            

            Inversor inversor = _context.Users.OfType<Inversor>().FirstOrDefault<Inversor>(p => p.UserName.Equals(User.Identity.Name));

            int[] idsInversion;
            ModelState.Clear();

            foreach (InversionCreateViewModel itemInversion in inversionCreate.inversiones)
            {
                proyecto = await _context.Proyecto.FirstOrDefaultAsync <Proyecto>(m => m.ProyectoId == itemInversion.inversion.Proyecto.ProyectoId);                
                inversion = new Inversion();                
                //float MinInv = proyecto.MinInversion;


                if (itemInversion.Cuota.CompareTo(itemInversion.MinInver) <= 0)
                {
                    ModelState.AddModelError("Cuota incorrecta", "Por favor, introduzca una couta válida.");
                }
                else
                {
                    itemInversion.inversion.Cuota= itemInversion.Cuota;                    
                    itemInversion.inversion.Intereses = (float) itemInversion.Interes;
                    itemInversion.inversion.Inversor = inversor;
                    
                    if (itemInversion.TiposInversionSelected == "Business Angels")
                    {
                        itemInversion.inversion.TipoInversionesId = 1;
                    }
                    else if (itemInversion.TiposInversionSelected == "Crownfunding")
                    {
                        itemInversion.inversion.TipoInversionesId = 2;
                    }
                    else
                    {
                        itemInversion.inversion.TipoInversionesId = 3;
                    }
                    
                    itemInversion.inversion.EstadosInversiones = "En Curso";

                    //inversion.TipoInversiones = _context.TiposInversiones.Where(r => r.Nombre.Equals(itemInversion.tipoinversion)).FirstOrDefault();

                    itemInversion.inversion.Total = (itemInversion.Cuota * (itemInversion.Interes / 100)) + itemInversion.Cuota;

                    //itemInversion.inversion.Inversor.Monedero.Dinero = inversion.Inversor.Monedero.Dinero - (decimal)inversion.Cuota;
                    //itemInversion.inversion.Inversor.Monedero = inversion.Inversor.Monedero;

                    _context.Add(itemInversion.inversion);

                }
            }

            if (ModelState.ErrorCount > 0)
            {
                inversionCreate.Name = inversor.Nombre;
                inversionCreate.FirstSurname = inversor.Apellido1;
                inversionCreate.SecondSurname = inversor.Apellido2;
                ViewBag.Cuota = new SelectList(_context.Inversion.Select(c => c.Cuota).Distinct());
                ViewBag.TiposInversiones = new SelectList(_context.ProyectoTiposInversiones.Select(c => c.TiposInversiones.Nombre).Distinct());


                return View(inversionCreate);
            }

            await _context.SaveChangesAsync();

            idsInversion = new int[inversionCreate.inversiones.Count];
            
            for (int i = 0; i < idsInversion.Length; i++)
                idsInversion[i] = inversionCreate.inversiones[i].inversion.InversionId;
            
            InversionDetailsViewModel detailsViewModel = new InversionDetailsViewModel();
            detailsViewModel.ids = idsInversion;

            return RedirectToAction("Details", detailsViewModel);
        }

        // GET: Inversions/Details/
        public async Task<IActionResult> Details(InversionDetailsViewModel idmodel)
        {
            int[] ids = idmodel.ids;
            if (ids.Length == 0)
            {
                return NotFound();
            }

            var inversion = _context.Inversion.Include(s => s.Proyecto).
                ThenInclude<Inversion, Proyecto, Rating>(s => s.Rating).
                Where(s => ids.Contains(s.InversionId)).ToList();



            if (inversion.Count == 0)
            {
                return NotFound();
            }

            return View(inversion);
        }

        public IActionResult ResumeInversiones(int[] ids)
        {
            if (ids.Length == 0)
            {
                return NotFound();
            }
            List<Inversion> inversiones = new List<Inversion>();
            foreach (int idInversion in ids)
            {
                inversiones.Add(_context.Inversion.Include(s => s.Proyecto).ThenInclude<Inversion, Proyecto, Rating>(s => s.Rating)
                    .Where(s => s.InversionId == idInversion).First());
            }

            if (inversiones.Count == 0)
            {
                return NotFound();
            }
            ViewBag.inversiones = inversiones;
            return View(inversiones);
        }

        // GET: Inversions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inversion.Include(i => i.Inversor).Include(i => i.Proyecto).Include(i => i.TipoInversiones);
            return View(await applicationDbContext.ToListAsync());
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