using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Jiru.WebTest
{
    [TestClass]
    public class ControladorProyectoTest
    {
        private readonly int ID_PROYECTO = 351;

        Mock<ILogicaProyecto> mockLogicaProyecto;

        Mock<IHttpContextAccessor> mockHttpContextAccessor;

        ControladorProyecto controladorProyecto;

        [TestInitialize]
        public void Inicializar()
        {
            mockLogicaProyecto = new Mock<ILogicaProyecto>();

            ProyectoDTO proyecto = new ProyectoDTO()
            {
                Id = ID_PROYECTO,
                Nombre = "Twitch",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>()
            };
            mockLogicaProyecto.Setup(mock => mock.ObtenerProyecto(It.IsAny<int>())).Returns(proyecto);

            mockLogicaProyecto.Setup(mock => mock.ObtenerProyectos(It.IsAny<UsuarioDTO>()));

            mockLogicaProyecto.Setup(mock => mock.CrearProyecto(It.IsAny<ProyectoDTO>()));

            mockLogicaProyecto.Setup(mock => mock.EliminarProyecto(It.IsAny<int>()));

            mockLogicaProyecto.Setup(mock => mock.ModificarProyecto(It.IsAny<int>(), It.IsAny<ProyectoDTO>()));

            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            IDictionary<object, object> items = new Dictionary<object, object>();

            items.Add("usuario", new UsuarioDTO() { Id = 1, CorreoElectronico = "teslasapbe@gmail.com", Rol = "Administrador" });

            mockHttpContextAccessor.Setup(mock => mock.HttpContext).Returns(new DefaultHttpContext() { Items = items });

            controladorProyecto = new ControladorProyecto(mockLogicaProyecto.Object, mockHttpContextAccessor.Object);
        }

        [TestMethod]
        public void TestCrearProyecto()
        {
            ProyectoDTO proyecto = new ProyectoDTO()
            {
                Nombre = "YouTube",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>()
            };

            var resultado = controladorProyecto.Crear(proyecto);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.CrearProyecto(It.IsAny<ProyectoDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestEliminarProyecto()
        {
            var resultado = controladorProyecto.Eliminar(ID_PROYECTO);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.EliminarProyecto(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void TestModificarProyecto()
        {
            ProyectoDTO proyecto = new ProyectoDTO()
            {
                Nombre = "ElPeyoteAsesinoWeb",
                Bugs = new List<BugDTO>()
            };

            var resultado = controladorProyecto.Modificar(ID_PROYECTO, proyecto);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.ModificarProyecto(It.IsAny<int>(), It.IsAny<ProyectoDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignarDesarrollador()
        {
            var resultado = controladorProyecto.AsignarDesarrollador(1, "teslasapbe@gmail.com");

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignaTester()
        {
            var resultado = controladorProyecto.AsignarTester(1, "teslasapbe@gmail.com");

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestDesasignarDesarrollador()
        {
            var resultado = controladorProyecto.DesasignarDesarrollador(1, "teslasapbe@gmail.com");

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestDesasignarTester()
        {
            var resultado = controladorProyecto.DesasignarDesarrollador(1, "teslasapbe@gmail.com");

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }



        [TestMethod]
        public void TestObtenerProyecto()
        {
            var resultado = controladorProyecto.Obtener(ID_PROYECTO);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.ObtenerProyecto(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerProyectos()
        {
            var resultado = controladorProyecto.Obtener();

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaProyecto.Verify(mock => mock.ObtenerProyectos(It.IsAny<UsuarioDTO>()), Times.Once());
        }
    }
}
