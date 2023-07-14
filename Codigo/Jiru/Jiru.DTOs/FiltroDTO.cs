using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Jiru.DTOs
{
    public class FiltroDTO
    {
        public int ProyectoId { get; set; }

        public int Id { get; set; }

        public string Estado { get; set; }

        public string Nombre { get; set; }
    }
}
