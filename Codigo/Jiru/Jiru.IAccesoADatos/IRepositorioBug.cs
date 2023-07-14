using Jiru.Dominio;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jiru.IAccesoADatos
{
    public interface IRepositorioBug
    {
        public void Crear(Bug bug);
        public List<Bug> Obtener();
        public List<Bug> Obtener(Expression<Func<Bug, bool>> consultaConFiltros);
        public Bug Obtener(int id);
        public void Eliminar(int id);
        public void Modificar(Bug bug);
    }
}
