using FormularioAsistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class VerListaSolicitudesViewModel
    {
        public IEnumerable<Solicitud> Solicitudes { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Asistencia_Disponible Asistencia { get; set; }
    }
}
