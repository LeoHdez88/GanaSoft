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
    public class HaciendasController : Controller
    {
        private readonly GanaSoftDBContext _context;

        public HaciendasController(GanaSoftDBContext context)
        {
            _context = context;
        }

        // GET: Haciendas
        public async Task<IActionResult> Index()
        {
              return _context.Hacienda != null ? 
                          View(await _context.Hacienda.ToListAsync()) :
                          Problem("Entity set 'GanaSoftDBContext.Hacienda'  is null.");
        }

        // GET: Haciendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hacienda == null)
            {
                return NotFound();
            }

            var hacienda = await _context.Hacienda
                .FirstOrDefaultAsync(m => m.HaciendaId == id);
            if (hacienda == null)
            {
                return NotFound();
            }

            return View(hacienda);
        }

        // GET: Haciendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Haciendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaciendaId,nombre,Propietario,Direccion,Activo")] Hacienda hacienda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hacienda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hacienda);
        }

        // GET: Haciendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hacienda == null)
            {
                return NotFound();
            }

            var hacienda = await _context.Hacienda.FindAsync(id);
            if (hacienda == null)
            {
                return NotFound();
            }
            return View(hacienda);
        }

        // POST: Haciendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaciendaId,nombre,Propietario,Direccion,Activo")] Hacienda hacienda)
        {
            if (id != hacienda.HaciendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hacienda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaciendaExists(hacienda.HaciendaId))
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
            return View(hacienda);
        }

        // GET: Haciendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hacienda == null)
            {
                return NotFound();
            }

            var hacienda = await _context.Hacienda
                .FirstOrDefaultAsync(m => m.HaciendaId == id);
            if (hacienda == null)
            {
                return NotFound();
            }

            return View(hacienda);
        }

        // POST: Haciendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hacienda == null)
            {
                return Problem("Entity set 'GanaSoftDBContext.Hacienda'  is null.");
            }
            var hacienda = await _context.Hacienda.FindAsync(id);
            if (hacienda != null)
            {
                _context.Hacienda.Remove(hacienda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HaciendaExists(int id)
        {
          return (_context.Hacienda?.Any(e => e.HaciendaId == id)).GetValueOrDefault();
        }
    }
}
