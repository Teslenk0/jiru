using Jiru.DTOs;
using System.Collections.Generic;

namespace Jiru.ILogicaDominio
{
    public interface ILogicaBug
    {
        public void CrearBug(BugDTO bug, UsuarioDTO usuarioDTO);

        public void ModificarBug(int id, BugDTO bug, UsuarioDTO usuarioDTO);

        public void EliminarBug(int id, UsuarioDTO usuarioDTO);

        public BugDTO ObtenerBug(int id, UsuarioDTO usuarioDTO);

        public int ObtenerCantidadDeBugsPorUsuario(string correoElectronico);

        public List<BugDTO> ObtenerBugs(FiltroDTO filtros, UsuarioDTO usuarioDTO);
    }
}
