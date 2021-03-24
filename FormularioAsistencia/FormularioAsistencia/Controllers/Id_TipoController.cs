using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;

namespace FormularioAsistencia.Controllers
{
    public class Id_TipoController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public Id_TipoController(FormularioAsistenciaContext context)
        {
            _context = context;
        }

        // GET: Id_Tipo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Id_Tipo.ToListAsync());
        }

        // GET: Id_Tipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var id_Tipo = await _context.Id_Tipo
                .FirstOrDefaultAsync(m => m.TipoID == id);
            if (id_Tipo == null)
            {
                return NotFound();
            }

            return View(id_Tipo);
        }

        // GET: Id_Tipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Id_Tipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoID,Tipo")] Id_Tipo id_Tipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(id_Tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(id_Tipo);
        }

        // GET: Id_Tipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var id_Tipo = await _context.Id_Tipo.FindAsync(id);
            if (id_Tipo == null)
            {
                return NotFound();
            }
            return View(id_Tipo);
        }

        // POST: Id_Tipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoID,Tipo")] Id_Tipo id_Tipo)
        {
            if (id != id_Tipo.TipoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(id_Tipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Id_TipoExists(id_Tipo.TipoID))
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
            return View(id_Tipo);
        }

        // GET: Id_Tipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var id_Tipo = await _context.Id_Tipo
                .FirstOrDefaultAsync(m => m.TipoID == id);
            if (id_Tipo == null)
            {
                return NotFound();
            }

            return View(id_Tipo);
        }

        // POST: Id_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var id_Tipo = await _context.Id_Tipo.FindAsync(id);
            _context.Id_Tipo.Remove(id_Tipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Id_TipoExists(int id)
        {
            return _context.Id_Tipo.Any(e => e.TipoID == id);
        }
    }
}
