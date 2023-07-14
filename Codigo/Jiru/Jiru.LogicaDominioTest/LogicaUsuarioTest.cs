using Jiru.LogicaDominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Jiru.IAccesoADatos;
using Jiru.Configuracion;
using AutoMapper;
using Jiru.DTOs;
using Jiru.Dominio;
using Jiru.Excepciones.Base;

namespace Jiru.LogicaDominioTest
{
    [TestClass]
    public class LogicaUsuarioTest
    {
        UsuarioDTO usuarioAdministrador;

        UsuarioDTO usuarioDesarrollador;

        UsuarioDTO usuarioTester;

        UsuarioDTO usuarioExistente;

        LogicaUsuario logicaUsuario;

        Mock<IRepositorioUsuario> mockRepositorioUsuario;

        [TestInitialize]
        public void Inicializar()
        {
            usuarioAdministrador = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Administrador"
            };

            usuarioDesarrollador = new UsuarioDTO()
            {
                Nombre = "Lionel",
                Apellido = "Messi",
                NombreUsuario = "Pulga",
                Contrasena = "NeymarSosMalisimo10",
                CorreoElectronico = "leo.messi@psg.com.fr",
                Rol = "Desarrollador",
                CostoPorHora = 460
            };

            usuarioTester = new UsuarioDTO()
            {
                Nombre = "José",
                Apellido = "Rodriguez",
                NombreUsuario = "PepeRod",
                Contrasena = "MejorContrasena@1234_",
                CorreoElectronico = "pepe.rod@hotmail.com",
                Rol = "Tester",
                CostoPorHora = 320
            };

            usuarioExistente = new UsuarioDTO()
            {
                Id = 1,
                Nombre = "Nikola",
                Apellido = "Tesla",
                NombreUsuario = "Teslenk0",
                Contrasena = "teslasapbe@gmail.com",
                CorreoElectronico = "teslasapbe@gmail.com",
                Rol = "Administrador"
            };

            var perfilAutoMapper = new PerfilAutoMapper();

            var configuracionMapper = new MapperConfiguration(cfg => cfg.AddProfile(perfilAutoMapper));

            var mapper = new Mapper(configuracionMapper);

            mockRepositorioUsuario = new Mock<IRepositorioUsuario>();

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(usuarioExistente.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(() => null);

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(usuarioExistente.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(usuarioExistente));

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(usuarioExistente.Id)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(usuarioExistente));

            mockRepositorioUsuario.Setup(mock => mock.Crear(It.IsAny<Usuario>()));

            logicaUsuario = new LogicaUsuario(mockRepositorioUsuario.Object, mapper);
        }

        [TestMethod]
        public void TestCrearUsuarioYaExistente()
        {
            void accion() => logicaUsuario.CrearAdministrador(usuarioExistente);

            Assert.ThrowsException<ExcepcionUsuarioYaExistente>(accion);

            mockRepositorioUsuario.Verify(mock => mock.Crear(It.IsAny<Usuario>()), Times.Never());
        }

        [TestMethod]
        public void TestCrearUsuarioAdministrador()
        {
            logicaUsuario.CrearAdministrador(usuarioAdministrador);

            mockRepositorioUsuario.Verify(mock => mock.Crear(It.IsAny<Usuario>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearUsuarioTester()
        {
            logicaUsuario.CrearTester(usuarioTester);

            mockRepositorioUsuario.Verify(mock => mock.Crear(It.IsAny<Usuario>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearUsuarioDesarrollador()
        {
            logicaUsuario.CrearDesarrollador(usuarioDesarrollador);

            mockRepositorioUsuario.Verify(mock => mock.Crear(It.IsAny<Usuario>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerUsuarioPorId()
        {
            var resultado = logicaUsuario.ObtenerUsuario(usuarioExistente.Id);

            Assert.AreEqual(usuarioExistente.CorreoElectronico, resultado.CorreoElectronico);

            mockRepositorioUsuario.Verify(mock => mock.Obtener(It.IsAny<int>(), It.IsAny<bool>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerUsuarioPorCorreElectronico()
        {
            var resultado = logicaUsuario.ObtenerUsuario(usuarioExistente.CorreoElectronico);

            Assert.AreEqual(usuarioExistente.CorreoElectronico, resultado.CorreoElectronico);

            mockRepositorioUsuario.Verify(mock => mock.Obtener(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
        }
    }
}

