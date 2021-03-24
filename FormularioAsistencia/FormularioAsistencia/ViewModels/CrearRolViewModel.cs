using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class CrearRolViewModel
    {
        [Required]
        [Display(Name = "Rol")]
        public string Rol { get; set; }
    }
}
