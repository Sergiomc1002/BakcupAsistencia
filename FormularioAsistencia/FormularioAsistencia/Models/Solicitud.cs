using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormularioAsistencia.Models
{
    public class Solicitud
    {
        public int SolicitudId { get; set; }
        [Required(ErrorMessage = "El  nivel en el que se encuentra es requerido")]
        public string Nivel { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string Apellido1 { get; set; }
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string Apellido2 { get; set; }
        [Required(ErrorMessage = "Se requiere un correo válido")]
        public string CorreoSolicitante { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "No se permiten caracteres especiales")]
        [Required(ErrorMessage = "El número de carné  es requerido")]
        public string Carne { get; set; }
        [Required(ErrorMessage = "La carrera que cursa es requerida")]
        public string CarreraQueCursa { get; set; }
        [Required(ErrorMessage = "La cantidad de creditos matriculados es requerida")]
        public short NumeroDeCreditos { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Solo se permiten números")]
        [Required(ErrorMessage = "El número de cédula es requerido")]
        public string Cedula { get; set; }
        public long? NumeroDeCuenta { get; set; }
        public int? InformeDeMatricula { get; set; }
        public int? ExpedienteAcademico { get; set; }
        public int? FotocopiaCedula { get; set; }
        public short Semestre { get; set; }
        public int? Asistencia { get; set; }
        public int? CantidadHE { get; set; }
        public int? CantidadHA { get; set; }
        public int? Telefono1 { get; set; }
        public int? Telefono2 { get; set; }
        [Range(1, 10, ErrorMessage = "El promedio debe estar en un rango de 0 a 10")]
        [Required(ErrorMessage = "El promedio ponderado total es requerido")]
        public double Promedio { get; set; }
        public string Banco { get; set; }
        public Boolean CuentaBancaria { get; set; }
        public Boolean TieneHE { get; set; }
        public Boolean TieneHA { get; set; }
        public double PromedioRevisado { get; set; }
    }
}
