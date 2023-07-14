using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public UsuarioDTO Usuario { get; set; }

        public string Token { get; set; }
    }
}
