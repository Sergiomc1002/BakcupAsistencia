using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class Asistencia_Disponible
    {
        [Key]
        public int IdAsistencia { get; set; }
        public string Nombre { get; set; }
        public string Profesor { get; set; }
        public int? Grupos { get; set; }
    }
}
