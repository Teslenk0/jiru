using Jiru.Dominio;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jiru.IAccesoADatos
{
    public interface IRepositorioTarea
    {
        public void Crear(Tarea tarea);

        public List<Tarea> Obtener();
    }
}
