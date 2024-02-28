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
    public class TipoRazasController : Controller
    {
        private readonly GanaSoftDBContext _context;

        public TipoRazasController(GanaSoftDBContext context)
        {
            _context = context;
        }

        // GET: TipoRazas
        public async Task<IActionResult> Index()
        {
              return _context.TipoRaza != null ? 
                          View(await _context.TipoRaza.ToListAsync()) :
                          Problem("Entity set 'GanaSoftDBContext.TipoRaza'  is null.");
        }

        // GET: TipoRazas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoRaza == null)
            {
                return NotFound();
            }

            var tipoRaza = await _context.TipoRaza
                .FirstOrDefaultAsync(m => m.TipoRazaId == id);
            if (tipoRaza == null)
            {
                return NotFound();
            }

            return View(tipoRaza);
        }

        // GET: TipoRazas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoRazas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoRazaId,Descripcion,Activo")] TipoRaza tipoRaza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoRaza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRaza);
        }

        // GET: TipoRazas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoRaza == null)
            {
                return NotFound();
            }

            var tipoRaza = await _context.TipoRaza.FindAsync(id);
            if (tipoRaza == null)
            {
                return NotFound();
            }
            return View(tipoRaza);
        }

        // POST: TipoRazas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoRazaId,Descripcion,Activo")] TipoRaza tipoRaza)
        {
            if (id != tipoRaza.TipoRazaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoRaza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoRazaExists(tipoRaza.TipoRazaId))
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
            return View(tipoRaza);
        }

        // GET: TipoRazas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoRaza == null)
            {
                return NotFound();
            }

            var tipoRaza = await _context.TipoRaza
                .FirstOrDefaultAsync(m => m.TipoRazaId == id);
            if (tipoRaza == null)
            {
                return NotFound();
            }

            return View(tipoRaza);
        }

        // POST: TipoRazas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoRaza == null)
            {
                return Problem("Entity set 'GanaSoftDBContext.TipoRaza'  is null.");
            }
            var tipoRaza = await _context.TipoRaza.FindAsync(id);
            if (tipoRaza != null)
            {
                _context.TipoRaza.Remove(tipoRaza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoRazaExists(int id)
        {
          return (_context.TipoRaza?.Any(e => e.TipoRazaId == id)).GetValueOrDefault();
        }
    }
}
