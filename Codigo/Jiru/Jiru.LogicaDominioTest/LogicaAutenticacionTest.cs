using BCryptNet = BCrypt.Net.BCrypt;
using Jiru.LogicaDominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Jiru.IAccesoADatos;
using Jiru.Configuracion;
using AutoMapper;
using Jiru.DTOs;
using Jiru.Dominio;
using Jiru.Excepciones.Base;
using System;

namespace Jiru.LogicaDominioTest
{
    [TestClass]
    public class LogicaAutenticacionTest
    {
        UsuarioDTO usuario;
        
        LogicaAutenticacion logicaAutenticacion;

        Mock<IRepositorioUsuario> mockRepositorioUsuario;

        private readonly string TOKEN_FALSO = "ioqnefionqiernioqwe";

        private readonly string TOKEN_CORRECTO = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJDb3JyZW9FbGVjdHJvbmljbyI6InJpY2t5LmZvcnRAZ21haWwuY29tIiwibmJmIjoxNjMyNTg1MDU4LCJleHAiOjE2MzI1ODg2NTgsImlhdCI6MTYzMjU4NTA1OH0.ZDdbhpyZD40K2DaZtFWdonuPdJfxbretsu4h2vII1Mw";

        [TestInitialize]
        public void Inicializar()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            usuario = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Administrador"
            };

            var perfilAutoMapper = new PerfilAutoMapper();

            var configuracionMapper = new MapperConfiguration(cfg => cfg.AddProfile(perfilAutoMapper));

            var mapper = new Mapper(configuracionMapper);

            mockRepositorioUsuario = new Mock<IRepositorioUsuario>();

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(usuario.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(() => {

                    var u = mapper.Map<Usuario>(usuario);
                
                    u.Contrasena = BCryptNet.HashPassword(u.Contrasena);

                    return u;
                });

            logicaAutenticacion = new LogicaAutenticacion(mockRepositorioUsuario.Object, mapper);
        }

        [TestMethod]
        public void TestIniciarSesionDatosCorrectos()
        {
            var credenciales = new AutenticacionDTO()
            {
                CorreoElectronico = usuario.CorreoElectronico,
                Contrasena = usuario.Contrasena
            };

            var resultado = logicaAutenticacion.IniciarSesion(credenciales);

            ValidarCamposUsuario(usuario, resultado.Usuario);

            Assert.IsNotNull(resultado.Token);
        }

        [TestMethod]
        public void TestIniciarSesionUsuarioNoExistente()
        {
            var credenciales = new AutenticacionDTO()
            {
                CorreoElectronico = "teslasapbe@gmail.com",
                Contrasena = "testpassword123_"
            };

            void accion() => logicaAutenticacion.IniciarSesion(credenciales);

            Assert.ThrowsException<ExcepcionUsuarioInexistente>(accion);
        }

        [TestMethod]
        public void TestIniciarSesionContrasenaIncorrecta ()
        {
            var credenciales = new AutenticacionDTO()
            {
                CorreoElectronico = "ricky.fort@gmail.com",
                Contrasena = "123456789"
            };

            void accion() => logicaAutenticacion.IniciarSesion(credenciales);

            Assert.ThrowsException<ExcepcionContrasenaIncorrecta>(accion);
        }

        [TestMethod]
        public void TestValidarTokenIncorrecto()
        {
            var resultado = logicaAutenticacion.ValidarToken(TOKEN_FALSO);

            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void TestValidarTokenCorrecto()
        {
            var resultado = logicaAutenticacion.ValidarToken(TOKEN_CORRECTO);

            Assert.IsNotNull(resultado);

            ValidarCamposUsuario(usuario, resultado);
        }

        private void ValidarCamposUsuario(UsuarioDTO expected, UsuarioDTO resultado)
        {
            Assert.AreEqual(expected.Nombre, resultado.Nombre);
            Assert.AreEqual(expected.Apellido, resultado.Apellido);
            Assert.AreEqual(expected.NombreUsuario, resultado.NombreUsuario);
            Assert.AreEqual(expected.CorreoElectronico, resultado.CorreoElectronico);
            Assert.AreEqual(expected.Rol, resultado.Rol);
            Assert.IsNull(resultado.Contrasena);
        }

    }
}
