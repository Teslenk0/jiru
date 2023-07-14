using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionUsuarioYaExistente : Exception
    {
        public ExcepcionUsuarioYaExistente(string correoElectronico) : base($"El usuario {correoElectronico} ya existe") { }
    }
}
