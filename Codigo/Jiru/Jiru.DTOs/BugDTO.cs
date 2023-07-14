using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Jiru.DTOs
{
    public class BugDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre.")]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese el proyecto al que pertenece el bug.")]
        public int ProyectoId { get; set; }

        public ProyectoDTO Proyecto { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripcion.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una version.")]
        public string Version { get; set; }

        [RegularExpression("Activo|Resuelto", ErrorMessage = "El estado solo puede ser Activo o Resuelto")]
        [Required(ErrorMessage = "Por favor ingrese un estado.")]
        public string Estado { get; set; }

        public int? ResueltoPorId { get; set; }

        public UsuarioDTO ResueltoPor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese el usuario que reporta el bug.")]
        public int ReportadoPorId { get; set; }

        public UsuarioDTO ReportadoPor { get; set; }

        public string IdExterno { get; set; }

        public double DuracionHoras { get; set; }
    }
}
