using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormularioAsistencia.ViewModels
{
    public class SolicitudCreateViewModel
    {
        public Solicitud Solicitud { get; set; }
        public List<Asistencia_Disponible> Asistencias_Disponibles { get; set; }
        [BindProperty]
        public List<IFormFile> archivos { get; set; }
    }
}
