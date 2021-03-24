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
using FormularioAsistencia.Utilities;
using FormularioAsistencia.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO.Compression;
using System.Text;
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.Layout.Properties;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;


namespace FormularioAsistencia.Controllers
{
    [Authorize(Roles = "Administrativos, Secretaría, Profesores,Estudiante")]
    public class SolicitudController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public SolicitudController(FormularioAsistenciaContext context)
        {
            _context = context;
        }

        // GET: Solicitud

        public async Task<IActionResult> Index()
        {
            return View(await _context.Solicitud.ToListAsync());
        }
        [HttpGet]
        public IActionResult NuevoAño()
        {
            return View();
        }
        public IActionResult ConfirmacionAño ()
        {
            return View();
        }
        public async Task<IActionResult> Solicitudes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudes = await _context.Solicitud.Where(s => s.Asistencia == id).ToListAsync();

            return View(solicitudes);
        }

        // GET: Solicitud/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicitud/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            SolicitudCreateViewModel model = new SolicitudCreateViewModel();
            var asistencias = from a in _context.Asistencia_Disponible
                              select a;
            model.Asistencias_Disponibles = asistencias.ToList();

            return View(model);
        }

        // POST: Solicitud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("SolicitudId,Nivel,Nombre,Apellido1,Apellido2,CorreoSolicitante,Carne,CarreraQueCursa,NumeroDeCreditos,Cedula,NumeroDeCuenta,InformeDeMatricula,ExpedienteAcademico,FotocopiaCedula,Asistencia,CantidadHE,CantidadHA,Telefono1,Telefono2,Promedio,CuentaBancaria,TieneHE,TieneHA,Banco")] Solicitud solicitud, IFormCollection form, SolicitudCreateViewModel model)
        {
            try
            {
                ViewBag.mensaje = "";
                string stemp = Convert.ToString(form["hidden"]);
                SolicitudCreateViewModel model1 = new SolicitudCreateViewModel();
                var asis = await _context.Asistencia_Disponible.ToListAsync();
                model1.Asistencias_Disponibles = asis;
                if (stemp == "")
                {
                    ViewBag.mensaje = "Debe seleccionar al menos una asistencia";
                    return View(model1);
                }
                string[] asistencias = stemp.Split(",");
                Asistencia_Disponible asistenciaTemp;

                //SolicitudCreateViewModel model1 = new SolicitudCreateViewModel();
                //var asis = from a in _context.Asistencia_Disponible
                //           select a;
                //model1.Asistencias_Disponibles = asis.ToList();
                /*
                if (model == null) {
                    return View(model1);
                }
                */
                if (solicitud.CantidadHA != 0 && solicitud.CantidadHA != null)
                {
                    solicitud.TieneHA = true;
                }
                if (solicitud.CantidadHE != 0 && solicitud.CantidadHE != null)
                {
                    solicitud.TieneHE = true;
                }
                if (solicitud.NumeroDeCuenta == 0 || solicitud.NumeroDeCuenta == null)
                {
                    solicitud.CuentaBancaria = true;
                }
                List<PDF> pdfs = new List<PDF>();

                int contadorArchivos = 0;

                if (model.archivos != null)
                {
                    for (int i = 0; i < model.archivos.Count(); i++)
                    {
                        pdfs.Add(Archivo(model.archivos[i]));
                        contadorArchivos++;
                    }
                }
                Byte[] archivoGenerico = { 0 };
                while (contadorArchivos < 3)
                {
                    PDF pdf = new PDF();
                    using (var memoryStream = new MemoryStream(archivoGenerico))
                    {
                        pdf.PDF_File = memoryStream.ToArray();
                    }
                    contadorArchivos++;
                    pdfs.Add(pdf);
                }

                foreach (PDF file in pdfs)
                {
                    _context.Add(file);
                    await _context.SaveChangesAsync();
                }

                PDF archivoTemp1 = await _context.PDF
                    .FirstOrDefaultAsync(m => m.ID_PDF == pdfs[0].ID_PDF);
                PDF archivoTemp2 = await _context.PDF
                    .FirstOrDefaultAsync(m => m.ID_PDF == pdfs[1].ID_PDF);
                PDF archivoTemp3 = await _context.PDF
                    .FirstOrDefaultAsync(m => m.ID_PDF == pdfs[2].ID_PDF);

                if (ModelState.IsValid)
                {
                    short sem;
                    var list = _context.Semestre.OrderByDescending(a => a.Numero).ToList();
                    if (list.Count == 0)
                    {
                        sem = 1;
                    }
                    else
                    {
                        sem = list.FirstOrDefault().Numero;
                    }
                  
                    foreach (var item in asistencias)
                    {
                        Solicitud solicitudAdd = new Solicitud();
                        solicitudAdd.Nivel = solicitud.Nivel;
                        solicitudAdd.Nombre = solicitud.Nombre;
                        solicitudAdd.Apellido1 = solicitud.Apellido1;
                        solicitudAdd.Apellido2 = solicitud.Apellido2;
                        solicitudAdd.CorreoSolicitante = solicitud.CorreoSolicitante;
                        solicitudAdd.Carne = solicitud.Carne;
                        solicitudAdd.NumeroDeCreditos = solicitud.NumeroDeCreditos;
                        solicitudAdd.Cedula = solicitud.Cedula;
                        solicitudAdd.NumeroDeCuenta = solicitud.NumeroDeCuenta;
                        solicitudAdd.Semestre = sem;
                        solicitudAdd.FotocopiaCedula = archivoTemp1.ID_PDF;
                        solicitudAdd.ExpedienteAcademico = archivoTemp3.ID_PDF;
                        solicitudAdd.InformeDeMatricula = archivoTemp2.ID_PDF;
                        solicitudAdd.CarreraQueCursa = solicitud.CarreraQueCursa;
                        solicitudAdd.CantidadHE = solicitud.CantidadHE;
                        solicitudAdd.CantidadHA = solicitud.CantidadHA;
                        solicitudAdd.Telefono1 = solicitud.Telefono1;
                        solicitudAdd.Telefono2 = solicitud.Telefono2;
                        solicitudAdd.Promedio = solicitud.Promedio;
                        solicitudAdd.CuentaBancaria = solicitud.CuentaBancaria;
                        solicitudAdd.TieneHE = solicitud.TieneHE;
                        solicitudAdd.TieneHA = solicitud.TieneHA;
                        solicitudAdd.Banco = solicitud.Banco;


                        asistenciaTemp = await _context.Asistencia_Disponible
                    .FirstOrDefaultAsync(m => m.IdAsistencia == Int32.Parse(item));
                        solicitudAdd.Asistencia = asistenciaTemp.IdAsistencia;


                        _context.Add(solicitudAdd);
                        await _context.SaveChangesAsync();
                        solicitud.SolicitudId++;
                    }
                    return RedirectToAction("index", "home");
                }

                return View(model1);
            }
            catch (FormatException)
            {
                return NotFound();
                // Return? Loop round? Whatever.
            }


        }

        // GET: Solicitud/Edit/5
        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudId,Nivel,Nombre,Apellido1,Apellido2,CorreoSolicitante,Carne,CarreraQueCursa,NumeroDeCreditos,Cedula,NumeroDeCuenta,InformeDeMatricula,ExpedienteAcademico,FotocopiaCedula,Asistencia,CantidadHE,CantidadHA,Telefono1,Telefono2,Promedio,CuentaBancaria,TieneHE,TieneHA")] Solicitud solicitud)
        {
            if (id != solicitud.SolicitudId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.SolicitudId))
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
            return View(solicitud);
        }
        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> BorrarTodas(int? id)
        {
            var solicitudes = _context.Solicitud.Where(x => x.Asistencia == id);
            var archivos = new int[solicitudes.Count(), 3];
            for (int i = 0; i < solicitudes.Count(); i++)
            {
                var solicitud = solicitudes.ToList()[i];
                archivos[i,0] = (int)solicitud.ExpedienteAcademico;
                archivos[i,1] = (int)solicitud.FotocopiaCedula;
                archivos[i,2] = (int)solicitud.InformeDeMatricula;
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

            return RedirectToAction("VerLista", new { id_asistencia = id });
        }
        // GET: Solicitud/Delete/5
        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> Delete(int? id)
        {
            var solicitud = _context.Solicitud.Find(id);
            int[] archivos = new int[3];
            archivos[0] = (int)solicitud.ExpedienteAcademico;
            archivos[1] = (int)solicitud.FotocopiaCedula;
            archivos[2] = (int)solicitud.InformeDeMatricula;
            var solicitudArchivo = _context.Solicitud.Where(x => x.ExpedienteAcademico == archivos[0]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[0]));

            }
            solicitudArchivo = _context.Solicitud.Where(x => x.FotocopiaCedula == archivos[1]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[1]));

            }
            solicitudArchivo = _context.Solicitud.Where(x => x.InformeDeMatricula == archivos[2]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[2]));

            }
            _context.Solicitud.Remove(solicitud);


            await _context.SaveChangesAsync();

            return RedirectToAction("VerLista", new { id_asistencia = solicitud.Asistencia });
        }

        // POST: Solicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = _context.Solicitud.Find(id);
            int[] archivos = new int[3];
            archivos[0] = (int)solicitud.ExpedienteAcademico;
            archivos[1] = (int)solicitud.FotocopiaCedula;
            archivos[2] = (int)solicitud.InformeDeMatricula;
            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();
            var solicitudArchivo = _context.Solicitud.Where(x => x.ExpedienteAcademico == archivos[0]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[0]));

            }
            solicitudArchivo = _context.Solicitud.Where(x => x.FotocopiaCedula == archivos[1]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[1]));

            }
            solicitudArchivo = _context.Solicitud.Where(x => x.InformeDeMatricula == archivos[2]);
            if (solicitudArchivo.Count() == 0)
            {
                _context.PDF.Remove(_context.PDF.Find(archivos[2]));

            }

            await _context.SaveChangesAsync();



            return RedirectToAction("VerLista", new { id_asistencia = solicitud.Asistencia });
        }

        private bool SolicitudExists(int id)
        {
            return _context.Solicitud.Any(e => e.SolicitudId == id);
        }

        private PDF Archivo(IFormFile file) {
            PDF pdf = new PDF();
            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);

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
            return pdf;
        }

        [HttpGet()]
        public ActionResult generarPDF(string temp_carnes, string temp_apellidos1, string temp_apellidos2, string temp_nombres, string temp_funciones, string temp_grupos)
        {
            string string_pdf;
            //FileStreamResult file;

            string[] stemp_carnes = temp_carnes.Split(",");
            string[] stemp_apellidos1 = temp_apellidos1.Split(",");
            string[] stemp_apellidos2 = temp_apellidos2.Split(",");
            string[] stemp_nombres = temp_nombres.Split(",");
            string[] stemp_funciones = temp_funciones.Split(",");
            string[] stemp_grupos = temp_grupos.Split(",");

            //Encoding encoding = Encoding.Unicode;

            string_pdf = "\tEstudiantes elegidos en el proceso de selección de asistentes\n\n";

            for (int i = 0; i < stemp_carnes.Length; ++i)
            {
                string_pdf += "\tCarné: " + stemp_carnes[i] + "\n";
                string_pdf += "\tNombre: " + stemp_nombres[i] + " " + stemp_apellidos1[i] + " " + stemp_apellidos2[i] + "\n";
                string_pdf += "\tGrupo Asignado: " + stemp_grupos[i] + "\n";
                string_pdf += "\tFunciones que var a realizar el estudiante: " + stemp_funciones[i] + "\n\n\n";
            }

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            graphics.DrawString(string_pdf, font, PdfBrushes.Black, new PointF(0, 0));
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "Seleccionados.pdf";
            return fileStreamResult;

            //byte[] file;

            //var stream = new MemoryStream();
            //var writer = new PdfWriter(stream);
            //var pdf = new PdfDocument(writer);
            //var document = new Document(pdf);

            //document.Add(new Paragraph(string_pdf));

            //Encoding encoding = Encoding.Unicode;
            //archivos.Add(new ZipItem(solicitud.Carne + ".csv", csv, encoding));

            //var byteArray = encoding.GetBytes(string_pdf);
            //var memoryStream = new MemoryStream(byteArray);
            //stream.Position = 0;
            //document.Close();
            //FileStreamResult file = File(stream, "application/pdf");
            //;
            //file.FileDownloadName = "Seleccionados.pdf";
            //return file;
        }
        /*
        [HttpGet()]
        public FileStreamResult generarPDF(IFormCollection form)
        {
            string string_pdf;
            //FileStreamResult file;
            string temp_carnes = Convert.ToString(form["carnes"]);
            string temp_apellidos1 = Convert.ToString(form["apellidos1"]);
            string temp_apellidos2 = Convert.ToString(form["apellidos2"]);
            string temp_nombres = Convert.ToString(form["nombres"]);
            string temp_funciones = Convert.ToString(form["funciones"]);
            string temp_grupos = Convert.ToString(form["grupos"]);
            string[] stemp_carnes = temp_carnes.Split(".-\n");
            string[] stemp_apellidos1 = temp_apellidos1.Split(".-\n");
            string[] stemp_apellidos2 = temp_apellidos2.Split(".-\n");
            string[] stemp_nombres = temp_nombres.Split(".-\n");
            string[] stemp_funciones = temp_funciones.Split(".-\n");
            string[] stemp_grupos = temp_grupos.Split(".-\n");

            Encoding encoding = Encoding.Unicode;

            string_pdf = "\tEstudiantes elegidos en el proceso de selección de asistentes\n\n";

            for (int i = 0; i < stemp_carnes.Length; ++i)
            {
                string_pdf += "\tCarné: " + stemp_carnes[i] + "\n";
                string_pdf += "\tNombre: " + stemp_nombres[i] + " " + stemp_apellidos1[i] + " " + stemp_apellidos2[i] + "\n";
                string_pdf += "\tGrupo Asignado: " + stemp_grupos[i] + "\n";
                string_pdf += "\tFunciones que var a realizar el estudiante: " + stemp_funciones[i] + "\n";
            }

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            graphics.DrawString(string_pdf, font, PdfBrushes.Black, new PointF(0, 0));
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "Seleccionados.pdf";
            return fileStreamResult;
            
        }
        */
        [HttpGet()]
        public async Task<IActionResult> Descargar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Solicitud solicitud = await _context.Solicitud
               .FirstOrDefaultAsync(m => m.SolicitudId == id);
            var matricula = await _context.PDF
               .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.InformeDeMatricula);
            var expediente = await _context.PDF
              .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.ExpedienteAcademico);
            var fotocopia = await _context.PDF
              .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.FotocopiaCedula);
            List<ZipItem> archivos = new List<ZipItem>();
            ZipItem archivoMatricula = new ZipItem(solicitud.Carne + " matricula.pdf", matricula.PDF_File);
            archivos.Add(archivoMatricula);
            archivos.Add(new ZipItem(solicitud.Carne + " expediente.pdf", expediente.PDF_File));
            archivos.Add(new ZipItem(solicitud.Carne + " fotocopia.pdf", fotocopia.PDF_File));

            string csv = "Carné, Nombre, Primer Apellido, Segundo Apellido, Promedio, Cédula, Teléfono 1, Teléfono 2, Correo, Carrera, Nivel, Número " +
                "de créditos, Promedia, Cuenta Bancaria, Banco, Número de Cuenta, Asistenca, Tiene Horas Asistente en otra unidad, Cantidad de Horas Asistente, " +
                "Tiene Horas Estudiantes en otra unidad, Cantidad de Horas Estudiante\n";

            csv += solicitud.Carne + " ," + solicitud.Nombre + " ," + solicitud.Apellido1 + " ," + solicitud.Apellido2 + " ," + solicitud.Promedio + " ," +
                solicitud.Cedula + " ," + solicitud.Telefono1 + " ," + solicitud.Telefono2 + " ," + solicitud.CorreoSolicitante + " ," + solicitud.CarreraQueCursa + " ," +
                solicitud.Nivel + " ," + solicitud.NumeroDeCreditos + " ," + solicitud.Promedio + " ," + solicitud.CuentaBancaria + " ," + solicitud.Banco + " ," + solicitud.NumeroDeCuenta +
                " ," + solicitud.Asistencia + " ," + solicitud.TieneHA + " ," + solicitud.CantidadHA + " ," + solicitud.TieneHE + " ," + solicitud.CantidadHE + "\n";

            Encoding encoding = Encoding.ASCII;
            archivos.Add(new ZipItem(solicitud.Carne + ".csv", csv, encoding));

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
            file.FileDownloadName = solicitud.Carne + ".zip";
            return file;
        }

        [HttpGet]

        [Authorize(Roles = "Administrativos, Secretaría, Profesores")]
        public async Task<IActionResult> VerLista(int id_asistencia,int semestre,string orden)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(orden) ? "name_desc" : "";
            ViewData["PromSortParm"] = orden == "Prom" ? "prom_desc" : "Prom";
            ViewData["CarneSortParm"] = orden == "Carne" ? "carne_desc" : "Carne";
            var model = new VerListaSolicitudesViewModel();
            IQueryable<Solicitud> solicitudes ;
            
                var asis = await _context.Asistencia_Disponible.FindAsync(id_asistencia);
                if (asis == null)
                {
                    return RedirectToAction("index", "Asistencia_Disponible");
                }
                model.Id = id_asistencia;
                model.Nombre = asis.Nombre;
                model.Asistencia = asis;

                solicitudes = _context.Solicitud.Where(x => (x.Asistencia == id_asistencia && x.Semestre == semestre));

            model.Solicitudes = orden switch
            {
                "name_desc" => solicitudes.OrderByDescending(x => x.Apellido1),
                "Prom" => solicitudes.OrderBy(x => x.Promedio),
                "prom_desc" => solicitudes.OrderByDescending(x => x.Apellido1),
                "Carne" => solicitudes.OrderBy(x => x.Carne),
                "carne_desc" => solicitudes.OrderByDescending(x => x.Carne),
                _ => solicitudes.OrderBy(x => x.Apellido1),
            };
            return View(model);
        }
        [HttpGet]

        [Authorize(Roles = "Administrativos, Secretaría")]
        public async Task<IActionResult> VerListaSemestral(int semestre)
        {
            var model = new VerListaSemestralViewModel();
            IQueryable<Solicitud> solicitudes;

            var asis =  await _context.Asistencia_Disponible.ToListAsync();
            if (asis == null)
            {
                return RedirectToAction("index", "Asistencia_Disponible");
            }
            model.Id = 0;
            model.Nombre = "Semestre "+ semestre.ToString();
            model.Asistencia = asis;

            solicitudes = (IQueryable<Solicitud>)await _context.Solicitud.Where(x => (x.Semestre == semestre)).OrderBy(x=>x.Asistencia).ThenBy(x=>x.Cedula).ToListAsync();

            model.Solicitudes = solicitudes;
           
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> VerListaPersonal()
        {
            var model = new VerListaPersonalViewModel();
            short sem;
            var list = await _context.Semestre.OrderByDescending(a => a.Numero).ToListAsync();
            if (list.Count == 0)
            {
                sem = 1;
            }
            else
            {
                sem = (short)(list.FirstOrDefault().Numero );
            }
            var solicitudes = await _context.Solicitud.Where(x => (x.CorreoSolicitante == User.Identity.Name && x.Semestre==sem)).ToListAsync();
            var asistencias = await _context.Asistencia_Disponible.Where(x => x.IdAsistencia != 0).ToListAsync();
            var cursos = new String[solicitudes.Count()];
            model.Solicitudes = solicitudes;
            model.Asistencias = asistencias;
            return View(model);
        }
        [HttpGet]
        public IActionResult VerMas(string id_a)
        {
            int id = Int32.Parse(id_a);
            var solicitud = _context.Solicitud.Find(id);

            if (solicitud == null)
            {
                ViewBag.ErrorMessage = $"Solicitud con ID = {id} no se puede encontrar.";
                return RedirectToAction("VerLista");

            }

            var model = new VerMasSolicitudViewModel
            {
                Solicitud = solicitud,
                Asistencia = _context.Asistencia_Disponible.Find(solicitud.Asistencia),
                archivos = null
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerMas(VerMasSolicitudViewModel model)
        {
            var solicitud = _context.Solicitud.Find(model.Solicitud.SolicitudId);

            if (solicitud == null)
            {
                ViewBag.ErrorMessage = $"Solicitud con ID = {model.Solicitud.SolicitudId} no se puede encontrar.";
                return RedirectToAction("VerLista", new { id_asistencia = solicitud.Asistencia });
            }
            else
            {
                solicitud.Nombre = model.Solicitud.Nombre;
                solicitud.Apellido1 = model.Solicitud.Apellido1;
                solicitud.Apellido2 = model.Solicitud.Apellido2;
                solicitud.Cedula = model.Solicitud.Cedula;
                solicitud.Carne = model.Solicitud.Carne;
                solicitud.Telefono1 = model.Solicitud.Telefono1;
                solicitud.Telefono2 = model.Solicitud.Telefono2;
                solicitud.CorreoSolicitante = model.Solicitud.CorreoSolicitante;
                solicitud.CarreraQueCursa = model.Solicitud.CarreraQueCursa;
                solicitud.Nivel = model.Solicitud.Nivel;
                solicitud.NumeroDeCuenta = model.Solicitud.NumeroDeCuenta;
                solicitud.CantidadHA = model.Solicitud.CantidadHA;
                solicitud.CantidadHE = model.Solicitud.CantidadHE;
                solicitud.PromedioRevisado = model.Solicitud.PromedioRevisado;

                List<PDF> pdfs = new List<PDF>();

                
                if (model.archivos != null)
                {
                    int contadorArchivos =  0;
                    for (int i = 0; i < model.archivos.Count(); i++)
                    {
                        pdfs.Add(Archivo(model.archivos[i]));
                        contadorArchivos++;
                    }
                
                    PDF pdf;
                    while (contadorArchivos > 0)
                    {
                        switch (contadorArchivos)
                        {
                            case 1:
                                pdf = await _context.PDF
                                     .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.InformeDeMatricula);
                                using (var memoryStream = new MemoryStream())
                                {
                                    await model.archivos[contadorArchivos-1].CopyToAsync(memoryStream);
                                    pdf.PDF_File = memoryStream.ToArray();
                                    _context.Update(pdf);
                                    await _context.SaveChangesAsync();
                                }
                                contadorArchivos--;
                                break;
                            case 2:
                                pdf = await _context.PDF
                                     .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.ExpedienteAcademico);
                                using (var memoryStream = new MemoryStream())
                                {
                                    await model.archivos[contadorArchivos-1].CopyToAsync(memoryStream);
                                    pdf.PDF_File = memoryStream.ToArray();
                                    _context.Update(pdf);
                                    await _context.SaveChangesAsync();
                                }
                                contadorArchivos--;
                                break;
                            case 3:
                                pdf = await _context.PDF
                                     .FirstOrDefaultAsync(m => m.ID_PDF == solicitud.FotocopiaCedula);
                                using (var memoryStream = new MemoryStream())
                                {
                                    await model.archivos[contadorArchivos-1].CopyToAsync(memoryStream);
                                    pdf.PDF_File = memoryStream.ToArray();
                                    _context.Update(pdf);
                                    await _context.SaveChangesAsync();
                                }
                                contadorArchivos--;
                                break;
                        }

                    }
                }
                if (model.Solicitud.NumeroDeCuenta != 0)
                {
                    solicitud.CuentaBancaria = false;
                }
                if (model.Solicitud.CantidadHA != 0)
                {
                    solicitud.TieneHA = true;
                }
                if (model.Solicitud.CantidadHE != 0)
                {
                    solicitud.TieneHE = true;
                }
                solicitud.NumeroDeCreditos = model.Solicitud.NumeroDeCreditos;
                solicitud.Promedio = model.Solicitud.Promedio;
                solicitud.Asistencia = model.Asistencia.IdAsistencia;
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.SolicitudId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("VerLista", new { id_asistencia = model.Asistencia.IdAsistencia });

            }

        }
        public async  Task<IActionResult> NuevoSemestre()
        {
            short sem;
            var list = _context.Semestre.OrderByDescending(a => a.Numero).ToList();
            if (list.Count == 0)
            {
                sem = 1;
            }
            else
            {
                sem = (short)(list.FirstOrDefault().Numero+1);
                

            }
            Semestre semestreNuevo = new Semestre();
                semestreNuevo.Numero = sem;

                _context.Add(semestreNuevo);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        
         public async Task<IActionResult> BorrarTodo() {
            var solicitudes = await _context.Solicitud.ToListAsync();
            foreach (var item in solicitudes) {
                _context.Solicitud.Remove(item);
            }
            await _context.SaveChangesAsync();
            var pdfs = await _context.PDF.ToListAsync();

            foreach (var item in pdfs) {
                _context.PDF.Remove(item);
            }
            await _context.SaveChangesAsync();

            var semestres = await _context.Semestre.ToListAsync();
            foreach (var item in semestres)
            {
                _context.Semestre.Remove(item);
            }
            await _context.SaveChangesAsync();
            Semestre semestreNuevo = new Semestre();
            semestreNuevo.Numero = 1;
            _context.Add(semestreNuevo);
            await _context.SaveChangesAsync();
            return RedirectToAction("ConfirmacionAño");

        }

    }
}
