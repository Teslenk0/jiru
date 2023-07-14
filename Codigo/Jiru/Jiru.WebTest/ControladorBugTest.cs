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
    public class ControladorBugTest
    {
        private readonly int ID_BUG = 351;

        private readonly int CANT_BUGS = 150;

        Mock<ILogicaBug> mockLogicaBug;

        Mock<IHttpContextAccessor> mockHttpContextAccessor;

        ControladorBug controladorBug;

        [TestInitialize]
        public void Inicializar()
        {
            mockLogicaBug = new Mock<ILogicaBug>();
          
            BugDTO bug = new BugDTO()
            {
                Id = ID_BUG,
                Nombre = "No carga la pagina de Haaland",
                ProyectoId = 1,
                Version = "0.1.0",
                Descripcion = "Erling me reporto que no le carga la pagina.",
                Estado = "Activo"
            };

            mockLogicaBug.Setup(mock => mock.ObtenerBug(It.IsAny<int>(), It.IsAny<UsuarioDTO>())).Returns(bug);

            mockLogicaBug.Setup(mock => mock.ObtenerCantidadDeBugsPorUsuario(It.IsAny<string>())).Returns(CANT_BUGS);

            mockLogicaBug.Setup(mock => mock.CrearBug(It.IsAny<BugDTO>(), It.IsAny<UsuarioDTO>()));

            mockLogicaBug.Setup(mock => mock.EliminarBug(It.IsAny<int>(), It.IsAny<UsuarioDTO>()));

            mockLogicaBug.Setup(mock => mock.ModificarBug(It.IsAny<int>(), It.IsAny<BugDTO>(), It.IsAny<UsuarioDTO>()));

            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            IDictionary<object, object> items = new Dictionary<object, object>();

            items.Add("usuario", new UsuarioDTO() { Id = 1, CorreoElectronico = "teslasapbe@gmail.com", Rol = "Administrador" });

            mockHttpContextAccessor.Setup(mock => mock.HttpContext).Returns(new DefaultHttpContext() { Items = items });

            controladorBug = new ControladorBug(mockLogicaBug.Object, mockHttpContextAccessor.Object);
        }

        [TestMethod]
        public void TestCrearBug()
        {
            BugDTO bug = new BugDTO()
            {
                Nombre = "Real Madrid contrata a Pochettino",
                ProyectoId = 1,
                Version = "0.1.0",
                Descripcion = "Pochettino arranca a perder partidos en el Madrid.",
                Estado = "Activo"
            };

            var resultado = controladorBug.Crear(bug);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.CrearBug(It.IsAny<BugDTO>(), It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestEliminarBug()
        {
            var resultado = controladorBug.Eliminar(ID_BUG);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.EliminarBug(It.IsAny<int>(), It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestModificarProyecto()
        {
            BugDTO bug = new BugDTO()
            {
                Nombre = "PSG-fake.com"
            };

            var resultado = controladorBug.Modificar(ID_BUG, bug);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.ModificarBug(It.IsAny<int>(), It.IsAny<BugDTO>(), It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerBug()
        {
            var resultado = controladorBug.Obtener(ID_BUG);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.ObtenerBug(It.IsAny<int>(), It.IsAny<UsuarioDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerCantidadDeBugsResueltosPorUsuario()
        {
            var resultado = controladorBug.ObtenerCantidadDeBugsPorDesarrollador("teslasapbe@hotmail.com");

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.ObtenerCantidadDeBugsPorUsuario(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerBugs()
        {
            var filtros = new FiltroDTO();

            var resultado = controladorBug.Obtener(filtros);

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaBug.Verify(mock => mock.ObtenerBugs(It.IsAny<FiltroDTO>(), It.IsAny<UsuarioDTO>()), Times.Once());
        }
    }
}
