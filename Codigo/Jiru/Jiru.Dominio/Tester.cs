using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Dominio
{
    public class Tester : Usuario
    {
        public virtual ICollection<Proyecto> Proyectos { get; set; }

        public double CostoPorHora { get; set; }

        public Tester()
        {
            Proyectos = new List<Proyecto>();
        }
    }
}
