using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionDatosIncorrectos : Exception
    {
        public ExcepcionDatosIncorrectos(string message) : base(message) { }
    }
}
