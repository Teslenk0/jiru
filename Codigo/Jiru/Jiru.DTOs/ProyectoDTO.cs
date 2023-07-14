using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiru.DTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre.")]
        public string Nombre { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CantBugs { get; set; }

        public List<BugDTO> Bugs { get; set; }

        public List<TareaDTO> Tareas { get; set; }

        public List<UsuarioDTO> Desarrolladores { get; set; }

        public List<UsuarioDTO> Testers { get; set; }

        public double Duracion { get; set; }

        public double Costo { get; set; }
    }
}
