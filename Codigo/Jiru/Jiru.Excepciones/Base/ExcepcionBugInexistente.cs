using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionBugInexistente : Exception
    {
        public ExcepcionBugInexistente(int id) : base($"El bug {id} no existe") { }
    }
}
