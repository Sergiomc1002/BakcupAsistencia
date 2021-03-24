using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class OlvideContraseñaViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el correo electrónico.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
