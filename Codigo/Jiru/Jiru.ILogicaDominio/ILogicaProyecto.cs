using Jiru.DTOs;
using System.Collections.Generic;

namespace Jiru.ILogicaDominio
{
    public interface ILogicaProyecto
    {
        public void CrearProyecto(ProyectoDTO proyecto);

        public void ImportarProyecto(ProyectoDTO proyecto, UsuarioDTO usuarioReportador, string correoUsuarioResuelve);

        public ProyectoDTO ObtenerProyecto(string nombre);

        public ProyectoDTO ObtenerProyecto(int id);

        public List<ProyectoDTO> ObtenerProyectos(UsuarioDTO usuarioDTO);

        public void ModificarProyecto(int id, ProyectoDTO proyectoDTO);

        public void AsignarUsuario(int id, string correoElectronico, string rol);

        public void DesasignarUsuario(int id, string correoElectronico, string rol);

        public void EliminarProyecto(int id);
    }
}
