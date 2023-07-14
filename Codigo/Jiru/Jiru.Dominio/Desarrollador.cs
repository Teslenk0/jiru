using System.Collections.Generic;

namespace Jiru.Dominio
{
    public class Desarrollador : Usuario
    {
        public virtual ICollection<Proyecto> Proyectos { get; set; }

        public double CostoPorHora { get; set; }

        public Desarrollador()
        {
            Proyectos = new List<Proyecto>();
        }
    }
}
