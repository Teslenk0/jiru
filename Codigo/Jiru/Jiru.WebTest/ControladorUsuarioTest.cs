using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jiru.WebTest
{
    [TestClass]
    public class ControladorUsuarioTest
    {

        Mock<ILogicaUsuario> mockLogicaUsuario;

        ControladorUsuario controladorUsuario;

        [TestInitialize]
        public void Inicializar()
        {
            mockLogicaUsuario = new Mock<ILogicaUsuario>();

            mockLogicaUsuario.Setup(mock => mock.CrearAdministrador(It.IsAny<UsuarioDTO>()));

            mockLogicaUsuario.Setup(mock => mock.CrearDesarrollador(It.IsAny<UsuarioDTO>()));

            mockLogicaUsuario.Setup(mock => mock.CrearTester(It.IsAny<UsuarioDTO>()));

            controladorUsuario = new ControladorUsuario(mockLogicaUsuario.Object);
        }

        [TestMethod]
        public void TestCrearUsuarioAdministrador()
        {
            UsuarioDTO usuarioAdministrador = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com"
            };

            var resultado = controladorUsuario.CrearAdministrador(usuarioAdministrador);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaUsuario.Verify(mock => mock.CrearAdministrador(It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearUsuarioDesarrollador()
        {
            UsuarioDTO usuarioDesarrollador = new UsuarioDTO()
            {
                Nombre = "Lionel",
                Apellido = "Messi",
                NombreUsuario = "Pulga",
                Contrasena = "NeymarSosMalisimo10",
                CorreoElectronico = "leo.messi@psg.com.fr",
                CostoPorHora = 95000
            };

            var resultado = controladorUsuario.CrearDesarrollador(usuarioDesarrollador);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaUsuario.Verify(mock => mock.CrearDesarrollador(It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearUsuarioTester()
        {
            UsuarioDTO usuarioTester = new UsuarioDTO()
            {
                Nombre = "José",
                Apellido = "Rodriguez",
                NombreUsuario = "PepeRod",
                Contrasena = "MejorContrasena@1234_",
                CorreoElectronico = "pepe.rod@hotmail.com",
                Rol = "Tester",
                CostoPorHora = 480
            };

            var resultado = controladorUsuario.CrearTester(usuarioTester);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaUsuario.Verify(mock => mock.CrearTester(It.IsAny<UsuarioDTO>()), Times.Once());
        }
    }
}
