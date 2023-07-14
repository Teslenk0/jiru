using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionUsuarioInexistente : Exception
    {
        public ExcepcionUsuarioInexistente(string correoElectronico) : base($"El usuario {correoElectronico} no existe") { }

        public ExcepcionUsuarioInexistente(int? id) : base($"El usuario {id} no existe") { }
    }
}
