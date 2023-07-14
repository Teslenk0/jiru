using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jiru.DTOs
{
    public class ImportacionDTO
    {

        [Required(ErrorMessage = "Por favor ingrese un proveedor.")]
        public string Proveedor { get; set; }

        public IDictionary<string, string> Parametros { get; set; }
    }
}
