using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class Anuncio

    {
        [Key]
        public int Id{ get; set; }
        [Required(ErrorMessage ="La fecha debe tener el formato dd/mm/aaaa")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd / MM / yyyy}")]
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
 
    }
}
