using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Dominio
{
    public class Tarea
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int ProyectoId { get; set; }

        public Proyecto Proyecto { get; set; }

        public double CostoPorHora { get; set; }

        public double DuracionHoras { get; set; }
    }
}
