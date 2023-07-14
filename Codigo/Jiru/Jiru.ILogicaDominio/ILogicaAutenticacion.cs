using Jiru.DTOs;

namespace Jiru.ILogicaDominio
{
    public interface ILogicaAutenticacion
    {
        public RespuestaAutenticacionDTO IniciarSesion(AutenticacionDTO credenciales);

        public UsuarioDTO ValidarToken(string token);
    }
}
