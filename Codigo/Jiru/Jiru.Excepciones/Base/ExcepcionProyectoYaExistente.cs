using System;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionProyectoYaExistente : Exception
    {
        public ExcepcionProyectoYaExistente(string nombre) : base($"El proyecto {nombre} ya existe") { }
    }
}
