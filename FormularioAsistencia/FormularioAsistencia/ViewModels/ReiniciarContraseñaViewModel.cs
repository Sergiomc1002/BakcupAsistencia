using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class ReiniciarContraseñaViewModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la  nueva contraseña.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña:")]
        public string Contraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña:")]
        [Compare("Contraseña", ErrorMessage =
            "Las contraseñas no coinciden")]
        public string Confirmacion { get; set; }

        public string token  { get; set; }
    }
}
