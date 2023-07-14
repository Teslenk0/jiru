using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.Dominio
{
    public class Bug
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int ProyectoId { get; set; }

        public Proyecto Proyecto { get; set; }

        public string Descripcion { get; set; }

        public string Version { get; set; }

        public Estado Estado { get; set; }

        public Usuario ResueltoPor { get; set; }

        public int? ResueltoPorId { get; set; }

        public Usuario ReportadoPor { get; set; }

        public int ReportadoPorId { get; set; }

        public string IdExterno { get; set; }

        public double DuracionHoras { get; set; }
    }
}
