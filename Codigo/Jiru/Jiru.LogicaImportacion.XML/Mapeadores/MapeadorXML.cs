
using Jiru.DTOs;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Jiru.LogicaImportacion.XML.ProveedorA.Mapeadores
{
    [XmlRoot("Empresa1")]
    public class MapeadorXML
    {
        public string Proyecto { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Bug")]
        public List<Bug> Bugs { get; set; }
    }

    public class Bug
    {
        [XmlElement(ElementName = "Id")]
        public string IdExterno { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Version { get; set; }

        public string Estado { get; set; }

        public double DuracionHoras { get; set; }
    }
}
