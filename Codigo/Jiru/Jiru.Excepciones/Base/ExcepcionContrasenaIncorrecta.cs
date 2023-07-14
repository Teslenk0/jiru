using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionContrasenaIncorrecta : Exception
    {
        public ExcepcionContrasenaIncorrecta() : base("La contraseña ingresada es incorrecta") { }
    }
}
