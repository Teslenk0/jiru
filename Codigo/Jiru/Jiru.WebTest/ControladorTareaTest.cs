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
    public class ControladorTareaTest
    {

        Mock<ILogicaTarea> mockLogicaTarea;

        ControladorTarea controladorTarea;

        Mock<IHttpContextAccessor> mockHttpContextAccessor;

        [TestInitialize]
        public void Inicializar()
        {
            mockLogicaTarea = new Mock<ILogicaTarea>();

            mockLogicaTarea.Setup(mock => mock.CrearTarea(It.IsAny<TareaDTO>()));

            mockLogicaTarea.Setup(mock => mock.ObtenerTareas(It.IsAny<UsuarioDTO>()));

            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            IDictionary<object, object> items = new Dictionary<object, object>();

            items.Add("usuario", new UsuarioDTO() { Id = 1, CorreoElectronico = "teslasapbe@gmail.com", Rol = "Administrador" });

            mockHttpContextAccessor.Setup(mock => mock.HttpContext).Returns(new DefaultHttpContext() { Items = items });

            controladorTarea = new ControladorTarea(mockLogicaTarea.Object, mockHttpContextAccessor.Object);
        }

        [TestMethod]
        public void TestCrearTarea()
        {
            TareaDTO tarea = new TareaDTO()
            {
                Nombre = "Correr a Pochettino del PSG",
                ProyectoId = 1,
                CostoPorHora = 100000,
                DuracionHoras = 1.2
            };

            var resultado = controladorTarea.Crear(tarea);

            var codigoRespuesta = ((CreatedResult)resultado).StatusCode;

            Assert.AreEqual(201, codigoRespuesta);

            mockLogicaTarea.Verify(mock => mock.CrearTarea(It.IsAny<TareaDTO>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerTareas()
        {
            var resultado = controladorTarea.Obtener();

            var codigoRespuesta = ((OkObjectResult)resultado).StatusCode;

            Assert.AreEqual(200, codigoRespuesta);

            mockLogicaTarea.Verify(mock => mock.ObtenerTareas(It.IsAny<UsuarioDTO>()), Times.Once());
        }
    }
}
