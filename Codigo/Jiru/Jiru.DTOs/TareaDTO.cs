using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Jiru.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre.")]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese el proyecto al que pertenece el bug.")]
        public int ProyectoId { get; set; }

        public ProyectoDTO Proyecto { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un costo por hora.")]
        public double CostoPorHora { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una duración en horas.")]
        public double DuracionHoras { get; set; }
    }
}
