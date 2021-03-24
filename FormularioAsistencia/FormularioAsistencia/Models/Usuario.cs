using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormularioAsistencia.Models
{
    public class Usuario
    {
        [Key]
        public int UserID { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ingrese un formato de correo electrónico válido.")]
        [Required(ErrorMessage = "Introduzca el correo electrónico.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Introduzca la contraseña.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Introduzca la confirmación de la contraseña.")]
        [Compare("Contraseña", ErrorMessage="Las contraseñas no coinciden.")]
        [NotMapped]
        public string Comparacion { get; set; }


        [Required]
        public int TipoUsuario { get; set; }

        
        [ForeignKey("TipoUsuario")]
        public virtual Id_Tipo Tipo { get; set; }

        [ForeignKey("TipoUsuario")]
        [NotMapped]
        public virtual List<Id_Tipo>Tipos { get; set; }


    }
}
