using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FormularioAsistencia.Models;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using MySql.EntityFrameworkCore;
using MySqlConnector;



namespace FormularioAsistencia.Data
{
    public class FormularioAsistenciaContext : IdentityDbContext
    {

        public FormularioAsistenciaContext (DbContextOptions<FormularioAsistenciaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }


        //usar este método si se va a trabajar con MySQL, de lo contrario comentar
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySQL("Server=127.0.0.1;port=3306;Database=datos_asistencias;Uid=asistencias;Pwd=8JjXrSA$bW");
            //optionsBuilder.UseMySql("Server=localhost:port=3306;Database=base;user=root;password=P4ssw0rd123");
        }
        
        public DbSet<FormularioAsistencia.Models.Solicitud> Solicitud { get; set; }

        public DbSet<FormularioAsistencia.Models.Asistencia_Disponible> Asistencia_Disponible { get; set; }

        // public DbSet<FormularioAsistencia.Models.Usuario> Usuario { get; set; }

        // public DbSet<FormularioAsistencia.Models.Id_Tipo> Id_Tipo { get; set; }
        ///public DbSet<FormularioAsistencia.Models.TipoUsuarioViewModel> TipoUsuarioViewModel { get; set; }
        public DbSet<FormularioAsistencia.Models.PDF> PDF { get; set; }
        public DbSet<FormularioAsistencia.Models.Anuncio> Anuncios { get; set; }
        public DbSet<FormularioAsistencia.Models.Semestre> Semestre { get; set; }
    }
}
