using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.LogicaImportacion.XML.ProveedorA.Mapeadores;
using Jiru.IImportacion;

namespace Jiru.LogicaImportacion.XML.ProveedorA
{
    public class LogicaImportacionXML : ILogicaImportacion
    {
        public LogicaImportacionXML(){}

        public ProyectoDTO ImportarBugs(IDictionary<string, string> parametros)
        {

            var archivo = parametros["archivo"];

            var datos = LeerYParsearArchivo(archivo);

            var proyecto =  new ProyectoDTO()
            {
                Nombre = datos.Proyecto,
                Bugs = datos.Bugs.Select(bug =>
                {

                    return new BugDTO()
                    {
                        Nombre = bug.Nombre,
                        Descripcion = bug.Descripcion,
                        Estado = bug.Estado,
                        IdExterno = bug.IdExterno,
                        Version = bug.Version,
                        DuracionHoras = bug.DuracionHoras
                    };

                }).ToList()
            };

            return proyecto;
        }

        private MapeadorXML LeerYParsearArchivo(string rutaArchivo)
        {
            try
            {
                Console.WriteLine("Archivo a importar: " + rutaArchivo);

                using (var streamArchivo = File.Open(rutaArchivo, FileMode.Open))
                {

                    XmlSerializer serializador = new XmlSerializer(typeof(MapeadorXML));

                    var datos = (MapeadorXML)serializador.Deserialize(streamArchivo);

                    return datos;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new ExcepcionArchivoOFormatoIncorrecto();
            }
        }

    }
}
