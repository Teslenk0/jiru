
namespace Jiru.Dominio
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public string CorreoElectronico { get; set; }

        public Rol Rol { get; set; }
    }
}
