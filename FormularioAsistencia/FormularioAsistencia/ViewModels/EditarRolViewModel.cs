using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class EditarRolViewModel
    {
        
            public EditarRolViewModel()
            {
                Users = new List<string>();
            }
            public string Id { get; set; }

            [Required(ErrorMessage = "Debe Tener un nombre")]
            public string Rol{ get; set; }

            public List<string> Users { get; set; }
        
    }
}
