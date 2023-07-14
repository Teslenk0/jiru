using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IImportacion;
using Jiru.ILogicaDominio;
using Jiru.LogicaImportacion.XML.ProveedorA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Jiru.LogicaImportacionTest
{
    [TestClass]
    public class LogicaImportacionXMLTest
    {
        private readonly string NOMBRE_XML = "C:\\importaciones\\Bugs.xml";

        private ILogicaImportacion logicaImportacion;

        [TestInitialize]
        public void Inicializar()
        {
            logicaImportacion = new LogicaImportacionXML();
        }

        [TestMethod]
        public void TestImportarBugsXML()
        {
            IDictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("archivo", NOMBRE_XML);

            var resultado = logicaImportacion.ImportarBugs(parametros);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void TestImportarBugsXMLArchivoInexistente()
        {
            IDictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("archivo", "NOMBRE_INEX");

            void accion() => logicaImportacion.ImportarBugs(parametros);

            Assert.ThrowsException<ExcepcionArchivoOFormatoIncorrecto>(accion);

        }
    }
}
