using Jiru.DTOs;
using System.Collections.Generic;


namespace Jiru.IImportacion
{
    public interface ILogicaImportacion
    {
        public ProyectoDTO ImportarBugs(IDictionary<string, string> parametros);
    }
}
