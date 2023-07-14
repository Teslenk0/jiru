using Jiru.AccesoADatos.Config;
using Jiru.Dominio;
using Jiru.IAccesoADatos;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Jiru.AccesoADatos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private JiruDbContext RepositorioContext;

        public RepositorioUsuario(JiruDbContext repositorioContext)
        {
            RepositorioContext = repositorioContext;
        }

        public void Crear(Usuario usuario)
        {
            RepositorioContext.Usuarios.Add(usuario);
            RepositorioContext.SaveChanges();
        }

        public Usuario Obtener(string correoElectronico, bool? conSeguimiento = true)
        {
            if (conSeguimiento != null && conSeguimiento == true)
            {
                return RepositorioContext.Usuarios
                .Include("Proyectos")
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }
            else
            {
                return RepositorioContext.Usuarios
                .Include("Proyectos")
                .AsNoTracking()
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }
                 
        }

        public Desarrollador ObtenerDesarrollador(string correoElectronico, bool? conSeguimiento = true)
        {
            if (conSeguimiento != null && conSeguimiento == true)
            {
                return RepositorioContext.Desarrolladores
                .Include("Proyectos")
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }
            else
            {
                return RepositorioContext.Desarrolladores
                .Include("Proyectos")
                .AsNoTracking()
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }
        }

        public Tester ObtenerTester(string correoElectronico, bool? conSeguimiento = true)
        {
            if (conSeguimiento != null && conSeguimiento == true)
            {
                return RepositorioContext
                    .Testers
                .Include("Proyectos")
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }
            else
            {
                return RepositorioContext.Testers
                .Include("Proyectos")
                .AsNoTracking()
                .Where(usuario => usuario.CorreoElectronico == correoElectronico)
                .FirstOrDefault();
            }

        }

        public Usuario Obtener(int? id, bool? conSeguimiento = true)
        {
            if (conSeguimiento != null && conSeguimiento == true)
            {
                return RepositorioContext
                    .Usuarios
                    .Include("Proyectos")
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
            }
            else
            {
                return RepositorioContext
                    .Usuarios
                    .Include("Proyectos")
                    .AsNoTracking()
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
            }

        }
    }
}
