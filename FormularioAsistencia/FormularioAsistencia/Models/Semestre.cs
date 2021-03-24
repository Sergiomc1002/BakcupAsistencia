using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class Semestre

    {
        [Key]
        public int Id{ get; set; }
        public short Numero { get; set; }
    }
}
