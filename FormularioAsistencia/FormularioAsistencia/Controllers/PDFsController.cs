using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FormularioAsistencia.Controllers
{   [Authorize]
    public class PDFsController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public PDFsController(FormularioAsistenciaContext context)
        {
            _context = context;
        }

        // GET: PDFs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PDF.ToListAsync());
        }

        // GET: PDFs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pDF = await _context.PDF
                .FirstOrDefaultAsync(m => m.ID_PDF == id);
            if (pDF == null)
            {
                return NotFound();
            }

            return View(pDF);
        }

        // GET: PDFs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PDFs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {

            PDF pdf = new PDF();

            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        pdf.PDF_File = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
                _context.Add(pdf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pdf);
        }

        // GET: PDFs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pDF = await _context.PDF.FindAsync(id);
            if (pDF == null)
            {
                return NotFound();
            }
            return View(pDF);
        }

        // POST: PDFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_PDF,PDF_File")] PDF pDF)
        {
            if (id != pDF.ID_PDF)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pDF);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PDFExists(pDF.ID_PDF))
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
            return View(pDF);
        }

        // GET: PDFs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pDF = await _context.PDF
                .FirstOrDefaultAsync(m => m.ID_PDF == id);
            if (pDF == null)
            {
                return NotFound();
            }

            return View(pDF);
        }

        // POST: PDFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pDF = await _context.PDF.FindAsync(id);
            _context.PDF.Remove(pDF);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PDFExists(int id)
        {
            return _context.PDF.Any(e => e.ID_PDF == id);
        }
    }
}
