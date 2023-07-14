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
    public class RepositorioTarea : IRepositorioTarea
    {
        private JiruDbContext RepositorioContext;

        public RepositorioTarea(JiruDbContext repositorioContext)
        {
            RepositorioContext = repositorioContext;
        }
        public void Crear(Tarea tarea)
        {
            RepositorioContext.Tareas.Add(tarea);

            RepositorioContext.SaveChanges();
        }

        public List<Tarea> Obtener()
        {
            return RepositorioContext.Tareas.Include("Proyecto").ToList();
        }
    }
}
