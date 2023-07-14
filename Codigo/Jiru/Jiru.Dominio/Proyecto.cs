using System;
using System.Collections.Generic;

namespace Jiru.Dominio
{
    public class Proyecto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<Tarea> Tareas { get; set; }

        public virtual ICollection<Desarrollador> Desarrolladores { get; set; }

        public virtual ICollection<Tester> Testers { get; set; }

        public Proyecto()
        {
            Desarrolladores = new List<Desarrollador>();
            Testers = new List<Tester>();
        }
    }
}
