using FormularioAsistencia.Utilites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class RegistrarEstudianteViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el correo electrónico.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [ValidEmail(dominio:"ucr.ac.cr", ErrorMessage = "El correo debe ser del dominio 'ucr.ac.cr'.")]
        public string Email { get; set; }


    }
}
