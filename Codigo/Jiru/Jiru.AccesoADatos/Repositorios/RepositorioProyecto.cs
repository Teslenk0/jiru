using Jiru.AccesoADatos.Config;
using Jiru.Dominio;
using Jiru.IAccesoADatos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Jiru.AccesoADatos.Repositorios
{
    public class RepositorioProyecto : IRepositorioProyecto
    {
        private JiruDbContext RepositorioContext;

        public RepositorioProyecto(JiruDbContext repositorioContext)
        {
            RepositorioContext = repositorioContext;
        }

        public void Crear(Proyecto proyecto)
        {
            RepositorioContext.Proyectos.Add(proyecto);

            RepositorioContext.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var proyecto = RepositorioContext.Proyectos.Find(id);

            RepositorioContext.Proyectos.Remove(proyecto);

            RepositorioContext.SaveChanges();
        }

        public void Modificar(Proyecto proyecto)
        {
            RepositorioContext.Proyectos.Update(proyecto);

            RepositorioContext.SaveChanges();
        }

        public Proyecto Obtener(int id)
        {
            return RepositorioContext.Proyectos
                .Include("Desarrolladores")
                .Include("Testers")
                .Include("Tareas")
                .Include("Bugs")
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public Proyecto Obtener(string nombre)
        {
            return RepositorioContext.Proyectos
                .Include("Desarrolladores")
                .Include("Testers")
                .Include("Tareas")
                .Include("Bugs")
                .Where(proyecto => proyecto.Nombre.ToUpper().Equals(nombre.ToUpper()))
                .FirstOrDefault();
        }

        public void AsignarUsuario(Proyecto proyecto, Usuario usuario, string rol)
        {
            if (rol == Rol.Desarrollador.ToString())
            {
                var desarrollador = RepositorioContext.Desarrolladores.Find(usuario.Id);
                desarrollador.Proyectos.Add(proyecto);
            }
            else if (rol == Rol.Tester.ToString())
            {
                var tester = RepositorioContext.Testers.Find(usuario.Id);
                tester.Proyectos.Add(proyecto);
            }

            RepositorioContext.SaveChanges();
        }

        public void DesasignarUsuario(Proyecto proyecto, Usuario usuario, string rol)
        {
            if (rol == Rol.Desarrollador.ToString())
            {
                var desarrollador = RepositorioContext.Desarrolladores.Find(usuario.Id);
                desarrollador.Proyectos.Remove(proyecto);
            }
            else if (rol == Rol.Tester.ToString())
            {
                var tester = RepositorioContext.Testers.Find(usuario.Id);
                tester.Proyectos.Remove(proyecto);
            }

            RepositorioContext.SaveChanges();
        }

        public List<Proyecto> Obtener()
        {
            return RepositorioContext.Proyectos
                .Include("Desarrolladores")
                .Include("Testers")
                .Include("Tareas")
                .Include("Bugs")
                .ToList();
        }
    }
}
