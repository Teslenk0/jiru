using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jiru.WebTest
{
    [TestClass]
    public class ControladorAutenticacionTest
    {
        Mock<ILogicaAutenticacion> mockLogicaAutenticacion;

        ControladorAutenticacion controladorAutenticacion;

        [TestInitialize]
        public void Inicializar()
        {
            mockLogicaAutenticacion = new Mock<ILogicaAutenticacion>();

            controladorAutenticacion = new ControladorAutenticacion(mockLogicaAutenticacion.Object);
        }

        [TestMethod]
        public void TestIniciarSesion()
        {
            AutenticacionDTO credenciales = new AutenticacionDTO()
            {
                Contrasena = "ElTesla123",
                CorreoElectronico = "ricky@gmail.com"
            };

            var resultado = controladorAutenticacion.Acceder(credenciales);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaAutenticacion.Verify(mock => mock.IniciarSesion(It.IsAny<AutenticacionDTO>()), Times.Once());
        }
    }
}
