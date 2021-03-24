using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class Id_Tipo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TipoID { get; set; }
        public string Tipo { get; set; }
    }
}
