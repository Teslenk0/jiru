using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IImportacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jiru.LogicaImportacion.JSON.ProveedorC
{
    public class LogicaImportacionJSON : ILogicaImportacion
    {
        public LogicaImportacionJSON() { }

        internal class ProyectoMapper
        {
            public string Proyecto { get; set; }

            public List<BugMapper> Bugs { get; set; }
        }

        internal class BugMapper
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Version { get; set; }
            public string Estado { get; set; }
            public double DuracionHoras { get; set; }
        }

        public ProyectoDTO ImportarBugs(IDictionary<string, string> parametros)
        {
            try
            {
                var rutaArchivo = parametros["archivo"];

                Console.WriteLine("Archivo a importar: " + rutaArchivo);

                StreamReader streamArchivo = new StreamReader(rutaArchivo);

                string jsonString = streamArchivo.ReadToEnd();

                ProyectoMapper proyecto = JsonConvert.DeserializeObject<ProyectoMapper>(jsonString);

                ProyectoDTO proyectoDTO = new ProyectoDTO()
                {
                    Nombre = proyecto.Proyecto,
                    Bugs = new List<BugDTO>()
                };

                proyecto.Bugs.ForEach((bug) =>
                {
                    proyectoDTO.Bugs.Add(new BugDTO()
                    {
                        Nombre = bug.Nombre,
                        Descripcion = bug.Descripcion,
                        Estado = bug.Estado,
                        DuracionHoras = bug.DuracionHoras,
                        IdExterno = bug.Id,
                        Version = bug.Version
                    });
                });

                return proyectoDTO;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new ExcepcionArchivoOFormatoIncorrecto();
            }
        }

    }
}
