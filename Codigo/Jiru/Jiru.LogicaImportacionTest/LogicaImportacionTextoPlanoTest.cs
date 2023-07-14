using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IImportacion;
using Jiru.ILogicaDominio;
using Jiru.LogicaImportacion.TXT.ProveedorB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Jiru.LogicaImportacionTest
{
    [TestClass]
    public class LogicaImportacionTXTTest
    {

        private readonly string NOMBRE_ARCHIVO = "C:\\importaciones\\Bugs.txt";

        private ILogicaImportacion logicaImportacion;

        private UsuarioDTO usuario;

        [TestInitialize]
        public void Inicializar()
        {
            usuario = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Administrador"
            };

            logicaImportacion = new LogicaImportacionTXT();
        }

        [TestMethod]
        public void TestImportarBugsTXTProyectoExistente()
        {
            IDictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("archivo", NOMBRE_ARCHIVO);

            var resultado = logicaImportacion.ImportarBugs(parametros);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void TestImportarBugsTXTArchivoInexistente()
        {
            IDictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("archivo", "NOMBRE_INEX");

            void accion() => logicaImportacion.ImportarBugs(parametros);

            Assert.ThrowsException<ExcepcionArchivoOFormatoIncorrecto>(accion);

        }
       
    }
}
