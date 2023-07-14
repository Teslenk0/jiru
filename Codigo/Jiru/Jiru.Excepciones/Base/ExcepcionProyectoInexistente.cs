using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionProyectoInexistente : Exception
    {
        public ExcepcionProyectoInexistente(int id) : base($"El proyecto {id} no existe") { }
    }
}
