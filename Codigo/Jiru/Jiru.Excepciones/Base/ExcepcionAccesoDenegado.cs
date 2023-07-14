using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionAccesoDenegado : Exception
    {
        public ExcepcionAccesoDenegado() : base($"Acceso denegado. No tienes permiso para realizar la acción solicitada.") { }
    }
}
