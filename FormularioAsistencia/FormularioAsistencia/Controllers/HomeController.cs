using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FormularioAsistencia.Models;
using FormularioAsistencia.Data;
using FormularioAsistencia.ViewModels;

namespace FormularioAsistencia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FormularioAsistenciaContext _context;

        public HomeController(ILogger<HomeController> logger, FormularioAsistenciaContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(VerAnunciosViewModel model)
        {

            model.Anuncios = _context.Anuncios.OrderBy(a => a.Fecha).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
