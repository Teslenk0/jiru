using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IImportacion;

namespace Jiru.LogicaImportacion.TXT.ProveedorB
{
    public class LogicaImportacionTXT : ILogicaImportacion
    {

        public LogicaImportacionTXT() { }

        public ProyectoDTO ImportarBugs(IDictionary<string, string> parametros)
        {
            try
            {
                var archivo = parametros["archivo"];

                Console.WriteLine($"Archivo a importar: {archivo}");

                StreamReader streamArchivo = new StreamReader(archivo);

                ProyectoDTO proyectoDTO = new ProyectoDTO();

                string linea;
                while ((linea = streamArchivo.ReadLine()) != null)
                {
                    if (proyectoDTO.Nombre == null)
                    {
                        proyectoDTO.Nombre = linea.Substring(0, 30).Trim();
                    }

                    if (proyectoDTO.Bugs == null)
                    {
                        proyectoDTO.Bugs = new List<BugDTO>();
                    }

                    var duracionHoras = linea.Substring(264, 4).Trim();

                    double parsedDuracionHoras = 0;

                    if (!String.IsNullOrEmpty(duracionHoras))
                    {
                        parsedDuracionHoras = double.Parse(duracionHoras);
                    }

                    proyectoDTO.Bugs.Add(new BugDTO()
                    {
                        IdExterno = linea.Substring(30, 4).Trim(),
                        Nombre = linea.Substring(34, 60).Trim(),
                        Descripcion = linea.Substring(94, 150).Trim(),
                        Version = linea.Substring(244, 10).Trim(),
                        Estado = linea.Substring(254, 10).Trim(),
                        DuracionHoras = parsedDuracionHoras
                    });
                }

                streamArchivo.Close();

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
