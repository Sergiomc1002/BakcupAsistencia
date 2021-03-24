using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class PDF
    {
        [Key]
        public int ID_PDF { get; set; }
        public Byte[] PDF_File { get; set; }
    }
}
