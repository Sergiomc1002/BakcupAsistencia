using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su correo electrónico.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar su contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
