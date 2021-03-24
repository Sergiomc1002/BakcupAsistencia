using FormularioAsistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class VerListaPersonalViewModel
    {
        public IEnumerable<Solicitud> Solicitudes { get; set; }
        public IEnumerable<Asistencia_Disponible> Asistencias { get; set; }
    }
}
