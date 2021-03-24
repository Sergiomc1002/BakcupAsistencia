using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public EditarUsuarioViewModel()
        {
            Roles = new List<IdentityRole>();
            Pertenece = new List<bool>();
        }
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<Boolean> Pertenece { get; set; }
    }
}
