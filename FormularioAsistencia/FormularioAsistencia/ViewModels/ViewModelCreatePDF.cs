using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.ViewModels
{
    public class ViewModelCreatePDF
    {
        public PDF pdf { get; set; }
        [BindProperty]
        public IFormFile file { get; set; }

    }
}
