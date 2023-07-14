using Jiru.DTOs;
using System.Collections.Generic;

namespace Jiru.ILogicaDominio
{
    public interface ILogicaTarea
    {
        public void CrearTarea(TareaDTO tarea);

        public List<TareaDTO> ObtenerTareas(UsuarioDTO usuarioDTO);
    }
}
