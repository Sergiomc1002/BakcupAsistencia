using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace FormularioAsistencia.Controllers
{
    public class SemestreController : Controller
    {
        private readonly FormularioAsistenciaContext _context;

        public SemestreController(FormularioAsistenciaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _context.Semestre.OrderBy(a => a.Numero).ToListAsync();
            ViewBag.Semestres = list;
            return View();

        }

    }
    
}
