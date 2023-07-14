using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionUsuarioNoAsignado : Exception
    {
        public ExcepcionUsuarioNoAsignado(string correoElectronico) : base($"El usuario {correoElectronico} no se encuentra asignado") { }
    }
}
