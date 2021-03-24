using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class CambiarContraseñaViewModel
    {
        [Required(ErrorMessage = "Debe ingresar la contraseña actual.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual:")]
        public string Actual{ get; set; }

        [Required(ErrorMessage = "Debe ingresar la  nueva contraseña.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña:")]
        public string NuevaContrasena { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña:")]
        [Compare("NuevaContrasena", ErrorMessage =
            "Las contraseñas no coinciden")]
        public string Confirmar { get; set; }
    }
}
