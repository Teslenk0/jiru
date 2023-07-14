using Jiru.AccesoADatos.Config;
using Jiru.Dominio;
using Jiru.IAccesoADatos;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Jiru.AccesoADatos.Repositorios
{
    public class RepositorioBug : IRepositorioBug
    {
        private JiruDbContext RepositorioContext;

        public RepositorioBug(JiruDbContext repositorioContext)
        {
            RepositorioContext = repositorioContext;
        }
        public void Crear(Bug bug)
        {
            RepositorioContext.Bugs.Add(bug);

            RepositorioContext.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var bug = RepositorioContext.Bugs.Find(id);

            RepositorioContext.Bugs.Remove(bug);

            RepositorioContext.SaveChanges();
        }

        public void Modificar(Bug bug)
        {
            RepositorioContext.SaveChanges();
        }

        public Bug Obtener(int id)
        {
            return RepositorioContext.Bugs.Find(id);
        }

        public List<Bug> Obtener()
        {
            return RepositorioContext.Bugs.ToList();
        }

        public List<Bug> Obtener(Expression<Func<Bug, bool>> consultaConFiltros)
        {
            return RepositorioContext.Bugs
                .Include("ResueltoPor")
                .Include("Proyecto")
                .Include("ReportadoPor")
                .Where(consultaConFiltros.Expand())
                .ToList();
        }
    }
}
