using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.DTOs
{
    public class RespuestaDTO
    {
        public int CodigoEstado { get; set; }
        public object Resultado { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
