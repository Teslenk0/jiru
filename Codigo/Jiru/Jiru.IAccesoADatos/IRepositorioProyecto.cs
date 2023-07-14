using Jiru.Dominio;
using System.Collections.Generic;

namespace Jiru.IAccesoADatos
{
    public interface IRepositorioProyecto
    {
        public void Crear(Proyecto proyecto);

        public Proyecto Obtener(int id);

        public Proyecto Obtener(string nombre);

        public List<Proyecto> Obtener();

        public void Eliminar(int id);

        public void Modificar(Proyecto proyecto);

        public void AsignarUsuario(Proyecto proyecto, Usuario usuario, string rol);

        public void DesasignarUsuario(Proyecto proyecto, Usuario usuario, string rol);
    }
}
