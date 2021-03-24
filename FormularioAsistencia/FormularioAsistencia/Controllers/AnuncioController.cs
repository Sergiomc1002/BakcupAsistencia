using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormularioAsistencia.Controllers
{
    public class AnuncioController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public AnuncioController(FormularioAsistenciaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            _context.Anuncios.Remove(anuncio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Create()
        {
            Anuncio anuncio=new Anuncio();
            anuncio.Fecha = DateTime.Now;

            return View(anuncio);
        }
        public async Task<IActionResult> Create([Bind("Id,Fecha,Descripcion")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(anuncio);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio= await _context.Anuncios.FindAsync(id);
            if (anuncio == null)
            {
                return NotFound();
            }
            return View(anuncio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Descripcion")] Anuncio anuncio)
        {
            if (id != anuncio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(anuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (AnuncioExists(anuncio.Id).Equals(false))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(anuncio);
        }
        private async Task<bool>  AnuncioExists(int id)
        {
            return await _context.Anuncios.AnyAsync(e => e.Id == id);
        }
    }
}
