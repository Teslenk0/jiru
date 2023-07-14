using Jiru.DTOs;
using System.Collections.Generic;

namespace Jiru.ILogicaDominio
{
    public interface ILogicaUsuario
    {
        public void CrearAdministrador(UsuarioDTO usuario);

        public void CrearDesarrollador(UsuarioDTO usuario);

        public void CrearTester(UsuarioDTO usuario);

        public UsuarioDTO ObtenerUsuario(string correoElectronico);

        public UsuarioDTO ObtenerUsuario(int id);
    }
}
