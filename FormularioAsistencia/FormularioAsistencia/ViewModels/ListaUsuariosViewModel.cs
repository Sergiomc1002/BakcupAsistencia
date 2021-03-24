using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class ListaUsuariosViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public List<IdentityUser> Users { get; set; }
        public List<List<Boolean>> Pertenece { get; set; }
    }
}
