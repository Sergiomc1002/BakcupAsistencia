using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Utilites
{
    public class ValidEmail:ValidationAttribute
    {
        private readonly string dominio;

        public ValidEmail(string dominio)
        {
            this.dominio = dominio;
        }
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == dominio.ToUpper();
            
        }
    }
}
