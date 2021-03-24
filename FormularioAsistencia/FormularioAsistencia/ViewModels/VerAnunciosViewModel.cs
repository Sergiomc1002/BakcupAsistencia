using FormularioAsistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class VerAnunciosViewModel
    {
        public IEnumerable<Anuncio> Anuncios { get; set; }
    }
}
