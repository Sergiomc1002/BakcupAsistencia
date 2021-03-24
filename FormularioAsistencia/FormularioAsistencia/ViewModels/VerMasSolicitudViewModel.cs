using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class VerMasSolicitudViewModel
    {
        public Solicitud Solicitud { get; set; }
        public Asistencia_Disponible Asistencia { get; set; }
        [BindProperty]
        public List<IFormFile> archivos { get; set; }

        public IFormFile matricula { get; set; }
        public IFormFile expediente { get; set; }
        public IFormFile fotocopia { get; set; }
    }
}
