using Jiru.Dominio;

namespace Jiru.IAccesoADatos
{
    public interface IRepositorioUsuario
    {
        public void Crear(Usuario usuario);

        public Usuario Obtener(string correoElectronico, bool? conSeguimiento = true);

        public Usuario Obtener(int? id, bool? conSeguimiento = true);

        public Desarrollador ObtenerDesarrollador(string correoElectronico, bool? conSeguimiento = true);

        public Tester ObtenerTester(string correoElectronico, bool? conSeguimiento = true);
    }
}
