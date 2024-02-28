using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GanaSoft.Data;
using GanaSoft.Models;

namespace GanaSoft.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly GanaSoftDBContext _context;

        public AnimalesController(GanaSoftDBContext context)
        {
            _context = context;
        }

        // GET: Animales
        public async Task<IActionResult> Index(string AnimalHacienda, string AnimalCodigo, string AnimalEstado, string AnimalRaza)
        {
            var Animales = await (
                                from a in _context.Animal
                                join e in _context.Estado on a.EstadoId equals e.EstadoId
                                join h in _context.Hacienda on a.HaciendaId equals h.HaciendaId
                                join t in _context.TipoRaza on a.TipoRazaId equals t.TipoRazaId

                                select
                                    new AnimalViewModel
                                    {
                                        AnimalId = a.AnimalId,
                                        Identificacion = a.Identificacion,
                                        Color = a.Color,
                                        TipoRazaId = a.TipoRazaId,
                                        NombreRaza = t.Descripcion,
                                        EstadoId = a.EstadoId,
                                        NombreEstado = e.Descripcion,
                                        HaciendaId = a.HaciendaId,
                                        NombreHacienda = h.nombre
                                    }).ToListAsync();

            // Usamos LINQ para obtener lista de generos
            List<string> Haciendaquery = (from m in Animales
                                          orderby m.NombreHacienda
                                          select m.NombreHacienda).ToList();
            List<string> Estadosquery = (from m in Animales
                                         orderby m.NombreEstado
                                         select m.NombreEstado).ToList();

            List<string> Razasquery = (from m in Animales
                                       orderby m.NombreRaza
                                       select m.NombreRaza).ToList();

            if (!string.IsNullOrEmpty(AnimalHacienda))
            {
                Animales = (Animales.Where(g => g.NombreHacienda == AnimalHacienda)).ToList();
            }

            if (!String.IsNullOrEmpty(AnimalCodigo))
            {
                Animales = (Animales.Where(g => g.Identificacion.Contains(AnimalCodigo))).ToList();
            }

            if (!String.IsNullOrEmpty(AnimalRaza))
            {
                Animales = (Animales.Where(g => g.NombreRaza == AnimalRaza)).ToList();
            }
            if (!String.IsNullOrEmpty(AnimalEstado))
            {
                Animales = (Animales.Where(g => g.NombreEstado == AnimalEstado)).ToList();
            }
            var filtrosAnimales = new FiltroAnimalesViewModel
            {
                Haciendas = new SelectList(Haciendaquery.Distinct().ToList()),
                Estados = new SelectList(Estadosquery.Distinct().ToList()),
                TiposRazas = new SelectList(Razasquery.Distinct().ToList()),
                Animales = Animales
            };

            return View(filtrosAnimales);

        }

        // GET: Animales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            /* var animal = await _context.Animal
                 .FirstOrDefaultAsync(m => m.AnimalId == id);*/

            var animal = await (
                              from a in _context.Animal
                              join e in _context.Estado on a.EstadoId equals e.EstadoId
                              join h in _context.Hacienda on a.HaciendaId equals h.HaciendaId
                              join t in _context.TipoRaza on a.TipoRazaId equals t.TipoRazaId
                              where a.AnimalId == id
                              select
                                  new AnimalViewModel
                                  {
                                      AnimalId = a.AnimalId,
                                      Identificacion = a.Identificacion,
                                      Color = a.Color,
                                      TipoRazaId = a.TipoRazaId,
                                      NombreRaza = t.Descripcion,
                                      EstadoId = a.EstadoId,
                                      NombreEstado = e.Descripcion,
                                      HaciendaId = a.HaciendaId,
                                      NombreHacienda = h.nombre
                                  }).FirstOrDefaultAsync();
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animales/Create
        public async Task<IActionResult> Create()
        {
            var estados = await _context.Estado.ToListAsync();
            var tiporazas = await _context.TipoRaza.ToListAsync();
            var haciendas = await _context.Hacienda.ToListAsync();

            ViewData["Estados"] = estados;
            ViewData["TipoRazas"] = tiporazas;
            ViewData["Haciendas"] = haciendas;
            return View();
        }

        // POST: Animales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Identificacion,Color,TipoRazaId,EstadoId,HaciendaId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);


            if (animal == null)
            {
                return NotFound();
            }

            var estados = await _context.Estado.ToListAsync();
            var tiporazas = await _context.TipoRaza.ToListAsync();
            var haciendas = await _context.Hacienda.ToListAsync();

            ViewData["Estados"] = estados;
            ViewData["TipoRazas"] = tiporazas;
            ViewData["Haciendas"] = haciendas;
            return View(animal);
        }

        // POST: Animales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Identificacion,Color,TipoRazaId,EstadoId,HaciendaId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            return View(animal);
        }

        // GET: Animales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            /* var animal = await _context.Animal
                 .FirstOrDefaultAsync(m => m.AnimalId == id);*/

            var animal = await (
                                from a in _context.Animal
                                join e in _context.Estado on a.EstadoId equals e.EstadoId
                                join h in _context.Hacienda on a.HaciendaId equals h.HaciendaId
                                join t in _context.TipoRaza on a.TipoRazaId equals t.TipoRazaId
                                where a.AnimalId == id
                                select
                                    new AnimalViewModel
                                    {
                                        AnimalId = a.AnimalId,
                                        Identificacion = a.Identificacion,
                                        Color = a.Color,
                                        TipoRazaId = a.TipoRazaId,
                                        NombreRaza = t.Descripcion,
                                        EstadoId = a.EstadoId,
                                        NombreEstado = e.Descripcion,
                                        HaciendaId = a.HaciendaId,
                                        NombreHacienda = h.nombre
                                    }).FirstOrDefaultAsync();
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'GanaSoftDBContext.Animal'  is null.");
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animal?.Any(e => e.AnimalId == id)).GetValueOrDefault();
        }
    }
}
