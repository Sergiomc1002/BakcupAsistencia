using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;
using FormularioAsistencia.Utilities;
using Microsoft.AspNetCore.Authorization;
using FormularioAsistencia.ViewModels;
using FormularioAsistencia.Migrations;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace FormularioAsistencia.Controllers
{
    [Authorize(Roles = "Administrativos, Secretaría,Profesores")]
    public class Asistencia_DisponibleController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public Asistencia_DisponibleController(FormularioAsistenciaContext context)
        {
            _context = context;
        }

        // GET: Asistencia_Disponible

        [Authorize(Roles = "Administrativos, Secretaría,Profesores")]
        public async Task<IActionResult> Index()
        {
            short sem;
            var list = _context.Semestre.OrderByDescending(a => a.Numero).ToList();
            if (list.Count == 0)
            {
                sem = 1;
            }
            else
            {
                sem = (short)(list.FirstOrDefault().Numero);
            }
            ViewBag.Semestre = sem;
            return View(await _context.Asistencia_Disponible.ToListAsync());
        }


        [Authorize(Roles = "Administrativos, Secretaría,Profesores")]
        // GET: Asistencia_Disponible/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia_Disponible = await _context.Asistencia_Disponible
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            //if (asistencia_Disponible == null)
            //{
                //return NotFound();
            //}

            return View(asistencia_Disponible);
        }


        // GET: Asistencia_Disponible/Create
        [Authorize(Roles = "Administrativos, Secretaría")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asistencia_Disponible/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsistencia,Nombre,Profesor,Grupos")] Asistencia_Disponible asistencia_Disponible)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia_Disponible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asistencia_Disponible);
        }

        // GET: Asistencia_Disponible/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia_Disponible = await _context.Asistencia_Disponible.FindAsync(id);
            if (asistencia_Disponible == null)
            {
                return NotFound();
            }
            return View(asistencia_Disponible);
        }

        // POST: Asistencia_Disponible/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsistencia,Nombre,Profesor,Grupos")] Asistencia_Disponible asistencia_Disponible)
        {
            if (id != asistencia_Disponible.IdAsistencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencia_Disponible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asistencia_DisponibleExists(asistencia_Disponible.IdAsistencia))
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
            return View(asistencia_Disponible);
        }

        // GET: Asistencia_Disponible/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia_Disponible = await _context.Asistencia_Disponible
                .FirstOrDefaultAsync(m => m.IdAsistencia == id);
            if (asistencia_Disponible == null)
            {
                return NotFound();
            }

            return View(asistencia_Disponible);
        }

        // POST: Asistencia_Disponible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistencia_Disponible = await _context.Asistencia_Disponible.FindAsync(id);

            var solicitudes = _context.Solicitud.Where(x => x.Asistencia == id);
            var archivos = new int[solicitudes.Count(), 3];
            for (int i = 0; i < solicitudes.Count(); i++)
            {
                var solicitud = solicitudes.ToList()[i];
                archivos[i, 0] = (int)solicitud.ExpedienteAcademico;
                archivos[i, 1] = (int)solicitud.FotocopiaCedula;
                archivos[i, 2] = (int)solicitud.InformeDeMatricula;
                _context.Solicitud.Remove(solicitud);
            }

            await _context.SaveChangesAsync();
            for (int i = 0; i < archivos.GetLength(0); i++)
            {
                var solicitudArchivo = _context.Solicitud.Where(x => x.ExpedienteAcademico == archivos[i, 0]).ToList();

                //await _context.SaveChangesAsync();
                if (solicitudArchivo.Count() == 0)
                {
                    _context.PDF.Remove(_context.PDF.Find(archivos[i, 0]));

                }
                solicitudArchivo = _context.Solicitud.Where(x => x.FotocopiaCedula == archivos[i, 1]).ToList();
                if (solicitudArchivo.Count() == 0)
                {
                    _context.PDF.Remove(_context.PDF.Find(archivos[i, 1]));

                }
                solicitudArchivo = _context.Solicitud.Where(x => x.InformeDeMatricula == archivos[i, 2]).ToList();
                if (solicitudArchivo.Count() == 0)
                {
                    _context.PDF.Remove(_context.PDF.Find(archivos[i, 2]));

                }


            }
            await _context.SaveChangesAsync();
            _context.Asistencia_Disponible.Remove(asistencia_Disponible);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool Asistencia_DisponibleExists(int id)
        {
            return _context.Asistencia_Disponible.Any(e => e.IdAsistencia == id);
        }


        [Authorize(Roles = "Administrativos, Secretaría,Profesores")]
        [HttpGet()]
        public async Task<IActionResult> Descargar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Asistencia_Disponible asistencia = await _context.Asistencia_Disponible
                .FirstOrDefaultAsync(a => a.IdAsistencia == id);

            var listaSolicitudes = await _context.Solicitud.Where(s => s.Asistencia == asistencia.IdAsistencia)
                .OrderByDescending(a=>a.Promedio).ToListAsync();
            List<int> listaPDFs = new List<int>();
            List<string> listaCarnes = new List<string>();
            string csv = "Carné, Nombre, Primer Apellido, Segundo Apellido, Promedio, Cédula, Teléfono 1, Teléfono 2, Correo, Carrera, Nivel, Número " +
                "de créditos, Promedia, Cuenta Bancaria, Número de Cuenta, Banco, Asistenca, Tiene Horas Asistente en otra unidad, Cantidad de Horas Asistente, "+
                "Tiene Horas Asistente en otra unidad, Cantidad de Horas Asistente\n";

            foreach (var item in listaSolicitudes) {
                csv += item.Carne + "," + item.Nombre + "," + item.Apellido1 + "," + item.Apellido2 + "," + item.Promedio + "," +
                item.Cedula + "," + item.Telefono1 + "," + item.Telefono2 + "," + item.CorreoSolicitante + "," + item.CarreraQueCursa + "," + 
                item.Nivel + "," + item.NumeroDeCreditos + "," + item.Promedio + "," + item.CuentaBancaria + ","+ item.Banco + "," + item.NumeroDeCuenta +
                "," + item.Asistencia + "," + item.TieneHA + "," + item.CantidadHA + "," + item.TieneHE + "," + item.CantidadHE + "\n";
                if (!listaPDFs.Contains((int)item.InformeDeMatricula))
                {
                    listaPDFs.Add((int)item.InformeDeMatricula);
                    listaCarnes.Add(item.Carne);
                }
                if (!listaPDFs.Contains((int)item.ExpedienteAcademico))
                    listaPDFs.Add((int)item.ExpedienteAcademico);
                if (!listaPDFs.Contains((int)item.FotocopiaCedula))
                    listaPDFs.Add((int)item.FotocopiaCedula);
            }

            List<ZipItem> archivos = new List<ZipItem>();
            int index = 0;

            for (int i = 0; i < listaPDFs.Count(); i += 3) {
                var matricula = await _context.PDF
               .FirstOrDefaultAsync(m => m.ID_PDF == listaPDFs[i]);
                var expediente = await _context.PDF
                  .FirstOrDefaultAsync(m => m.ID_PDF == listaPDFs[i+1]);
                var fotocopia = await _context.PDF
                  .FirstOrDefaultAsync(m => m.ID_PDF == listaPDFs[i+2]);


                archivos.Add(new ZipItem(listaCarnes[index] + " matricula.pdf", matricula.PDF_File));
                archivos.Add(new ZipItem(listaCarnes[index] + " expediente.pdf", expediente.PDF_File));
                archivos.Add(new ZipItem(listaCarnes[index] + " fotocopia.pdf", fotocopia.PDF_File));
                index++;
            }


            Encoding encoding = Encoding.ASCII;
            archivos.Add(new ZipItem("Lista de solicitudes " + asistencia.Nombre + ".csv", csv, encoding));


            var zipStream = new MemoryStream();
            using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var zipItem in archivos)
                {
                    var entry = zip.CreateEntry(zipItem.Name);
                    using (var entryStream = entry.Open())
                    {
                        zipItem.Content.CopyTo(entryStream);
                    }
                }
            }
            zipStream.Position = 0;
            FileStreamResult file = File(zipStream, "application/octet-stream");
            file.FileDownloadName = asistencia.Nombre + ".zip";

            return file;

            
        }
    }
}
