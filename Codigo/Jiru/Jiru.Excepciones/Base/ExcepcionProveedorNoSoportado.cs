using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionProveedorNoSoportado : Exception
    {
        public ExcepcionProveedorNoSoportado() : base($"Proveedor no soportado. Por favor contacte un administrador.") { }
    }
}
