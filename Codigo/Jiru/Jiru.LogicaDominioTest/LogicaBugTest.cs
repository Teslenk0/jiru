using Jiru.LogicaDominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Jiru.IAccesoADatos;
using Jiru.Configuracion;
using AutoMapper;
using Jiru.DTOs;
using Jiru.Dominio;
using Jiru.Excepciones.Base;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace Jiru.LogicaDominioTest
{
    [TestClass]
    public class LogicaBugTest
    {
        private readonly int BUG_ID = 1;

        private readonly int PROYECTO_ID = 1;

        BugDTO bug;

        ProyectoDTO proyecto;

        ProyectoDTO proyecto2;


        LogicaBug logicaBug;

        Mock<IRepositorioBug> mockRepositorioBug;

        Mock<IRepositorioProyecto> mockRepositorioProyecto;

        Mock<IRepositorioUsuario> mockRepositorioUsuario;

        IMapper mapper;

        UsuarioDTO usuarioDTO;

        [TestInitialize]
        public void Inicializar()
        {

            proyecto = new ProyectoDTO()
            {
                Id = 1,
                Nombre = "YouTube",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>()

            };


            bug = new BugDTO()
            {
                Id = BUG_ID,
                Nombre = "Error BD",
                ProyectoId = 1,
                Descripcion = "No carga BD",
                Version = "Primera",
                Estado = "Activo",
                DuracionHoras = 6
            };

            var perfilAutoMapper = new PerfilAutoMapper();

            var configuracionMapper = new MapperConfiguration(cfg => cfg.AddProfile(perfilAutoMapper));

            mapper = new Mapper(configuracionMapper);

            mockRepositorioBug = new Mock<IRepositorioBug>();

            Bug retornarBug()
            {

                var b = mapper.Map<Bug>(bug);

                b.Id = BUG_ID;

                return b;
            }

            mockRepositorioProyecto = new Mock<IRepositorioProyecto>();

            mockRepositorioUsuario = new Mock<IRepositorioUsuario>();

            Proyecto retornarProyecto()
            {

                var p = mapper.Map<Proyecto>(proyecto);

                p.Id = PROYECTO_ID;

                return p;
            }

            mockRepositorioBug.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(BUG_ID))))
                .Returns(retornarBug);

            mockRepositorioBug.Setup(mock => mock.Obtener(It.Is<int>(m => !m.Equals(BUG_ID))))
               .Returns(() => null);

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(PROYECTO_ID))))
                .Returns(retornarProyecto);

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => !m.Equals(PROYECTO_ID))))
               .Returns(() => null);

            mockRepositorioBug.Setup(mock => mock.Crear(It.IsAny<Bug>()));

            mockRepositorioBug.Setup(mock => mock.Eliminar(It.IsAny<int>()));

            mockRepositorioBug.Setup(mock => mock.Modificar(It.IsAny<Bug>()));

            logicaBug = new LogicaBug(mockRepositorioBug.Object, mockRepositorioProyecto.Object, mapper, mockRepositorioUsuario.Object);

            var proyectosSinBugs = new List<ProyectoDTO>();

            proyectosSinBugs.Add(proyecto);
            proyectosSinBugs.Add(new ProyectoDTO()
            {
                Id = 2,
                Nombre = "twitch",
            });

            usuarioDTO = new UsuarioDTO()
            {
                Id = 1,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester",
                Proyectos = proyectosSinBugs
            };
        }


        [TestMethod]
        public void TestCrearBug()
        {
            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.IsAny<int>(), It.IsAny<bool>())).Returns(mapper.Map<Usuario>(usuarioDTO));

            logicaBug.CrearBug(bug, usuarioDTO);

            mockRepositorioBug.Verify(mock => mock.Crear(It.IsAny<Bug>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearBugReportadoPorInexistente()
        {
            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.IsAny<int>(), It.IsAny<bool>())).Returns(() => null);

            void accion() => logicaBug.CrearBug(bug, usuarioDTO);

            Assert.ThrowsException<ExcepcionUsuarioInexistente>(accion);

            mockRepositorioBug.Verify(mock => mock.Crear(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestCrearBugProyectoInexistente()
        {
            bug.ProyectoId = 3;

            void accion() => logicaBug.CrearBug(bug, usuarioDTO);

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);
        }

        [TestMethod]
        public void TestEliminarBugExistente()
        {
            logicaBug.EliminarBug(BUG_ID, usuarioDTO);

            mockRepositorioBug.Verify(r => r.Eliminar(BUG_ID), Times.Once);
        }


        [TestMethod]
        public void TestEliminarBugInexistente()
        {
            void accion() => logicaBug.EliminarBug(123, usuarioDTO);

            Assert.ThrowsException<ExcepcionBugInexistente>(accion);
        }

        [TestMethod]
        public void TestModificarBugExistente()
        {
            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.IsAny<int>(), It.IsAny<bool>())).Returns(mapper.Map<Usuario>(usuarioDTO));

            var bugNuevo = new BugDTO()
            {
                ProyectoId = 1,
                Estado = "Resuelto",
                Descripcion = "NO ME CARGA LA PAGINAAAAA!!!!!!!!! RESOLVER PLS - PRIORIDAD 845 REALLY HIGH. Reporter: Apache",
                Nombre = "BOEL-1: No carga la pagina",
                Version = "1.0.0",
                ResueltoPorId = 1,
                DuracionHoras = 6
            };

            logicaBug.ModificarBug(BUG_ID, bugNuevo, usuarioDTO);

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Once());
        }

        [TestMethod]
        public void TestModificarBugResueltoPorInexistente()
        {
            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.IsAny<int>(), It.IsAny<bool>())).Returns(() => null);

            var bugNuevo = new BugDTO()
            {
                ProyectoId = 1,
                Estado = "Resuelto",
                Descripcion = "NO ME CARGA LA PAGINAAAAA!!!!!!!!! RESOLVER PLS - PRIORIDAD 845 REALLY HIGH. Reporter: Apache",
                Nombre = "BOEL-1: No carga la pagina",
                Version = "1.0.0",
                ResueltoPorId = 1,
                DuracionHoras = 2
            };

            void accion() => logicaBug.ModificarBug(BUG_ID, bugNuevo, usuarioDTO);

            Assert.ThrowsException<ExcepcionUsuarioInexistente>(accion);            

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestModificarBugInexistente()
        {
            void accion() => logicaBug.ModificarBug(123, null, usuarioDTO);

            Assert.ThrowsException<ExcepcionBugInexistente>(accion);
        }



        [TestMethod]
        public void TestModificarBugProyectoExistente()
        {
            proyecto2 = new ProyectoDTO()
            {
                Id = 2,
                Nombre = "twitch",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>()

            };

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(proyecto2.Id))))
                .Returns(() => mapper.Map<Proyecto>(proyecto2));

            var bugNuevo = new BugDTO()
            {
                ProyectoId = 2,
                Estado = "Activo",
                Descripcion = "NO ME CARGA LA PAGINAAAAA!!!!!!!!! RESOLVER PLS - PRIORIDAD 845 REALLY HIGH. Reporter: Apache",
                Nombre = "BOEL-1: No carga la pagina",
                Version = "1.0.0",
            };

            logicaBug.ModificarBug(BUG_ID, bugNuevo, usuarioDTO);

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Once());
        }
        [TestMethod]
        public void TestModificarBugProyectoInexistente()
        {
            var bugNuevo = new BugDTO()
            {
                Nombre = "Error BD",
                ProyectoId = 3,
                Descripcion = "No carga BD",
                Version = "Primera",
                Estado = "Activo",
                DuracionHoras = 2
            };

            bug.Estado = "Resuelto";
            bug.Descripcion = "NO ME CARGA LA PAGINAAAAA!!!!!!!!! RESOLVER PLS - PRIORIDAD 845 REALLY HIGH. Reporter: Apache";
            bug.Nombre = "BOEL-1: No carga la pagina";
            bug.Version = "1.0.0";

            mockRepositorioBug.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(BUG_ID))))
            .Returns(() => mapper.Map<Bug>(bug));

            void accion() => logicaBug.ModificarBug(BUG_ID, bugNuevo, usuarioDTO);

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestObtenerListaBugs()
        {
            var bugs = new List<BugDTO>();

            var proyectos = new List<ProyectoDTO>();

            bugs.Add(new BugDTO() { Id = 1, Nombre = "Esta todo roto", Estado = "Activo", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 2, Nombre = "roto, nada funciona", Estado = "Resuelto", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 3, Nombre = "Very roto", Estado = "Activo", ProyectoId = 8 });

            proyectos.Add(new ProyectoDTO()
            {
                Id = 5,
                Nombre = "TestProyecto",
                Bugs = bugs,
            });

            mockRepositorioBug.Setup(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()))
                .Returns(mapper.Map<List<Bug>>(bugs));

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Administrador",
                Proyectos = proyectos
            };

            var resultado = logicaBug.ObtenerBugs(null, usuarioDTO);

            Assert.AreEqual(bugs.Count, resultado.Count);

            ValidarListaBugs(bugs, resultado);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerListaBugsFiltradaProyectoPerteneciente()
        {
            var filtros = new FiltroDTO()
            {
                ProyectoId = 5,
                Nombre = "Todo roto",
                Estado = "Activo",
                Id = 1
            };

            var bugs = new List<BugDTO>();

            var proyectos = new List<ProyectoDTO>();


            bugs.Add(new BugDTO() { Id = 1, Nombre = "Esta todo roto", Estado = "Activo", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 2, Nombre = "roto, nada funciona", Estado = "Resuelto", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 3, Nombre = "Very roto", Estado = "Activo", ProyectoId = 5 });

            var mockResultado = bugs.Where(bug => bug.Id == filtros.Id &&
                                        bug.Nombre.ToUpper().Contains(filtros.Nombre.ToUpper()) &&
                                        bug.Estado == filtros.Estado &&
                                        bug.ProyectoId == filtros.ProyectoId);

            mockRepositorioBug.Setup(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()))
                .Returns(mapper.Map<List<Bug>>(mockResultado));

            proyectos.Add(new ProyectoDTO()
            {
                Id = 5,
                Nombre = "TestProyecto",
                Bugs = bugs,
            });

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester",
                Proyectos = proyectos,
            };

            var resultado = logicaBug.ObtenerBugs(filtros, usuarioDTO);

            Assert.AreEqual(mockResultado.ToList().Count, resultado.Count);

            ValidarListaBugs(mockResultado.ToList(), resultado);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Once());
        }


        [TestMethod]
        public void TestObtenerListaBugsFiltradaProyectoNoPerteneciente()
        {
            var filtros = new FiltroDTO()
            {
                ProyectoId = 5,
                Nombre = "Todo roto",
                Estado = "Activo",
                Id = 1
            };

            var bugs = new List<BugDTO>();

            bugs.Add(new BugDTO() { Id = 1, Nombre = "Esta todo roto", Estado = "Activo", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 2, Nombre = "roto, nada funciona", Estado = "Resuelto", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 3, Nombre = "Very roto", Estado = "Activo", ProyectoId = 5 });

            var mockResultado = bugs.Where(bug => bug.Id == filtros.Id &&
                                        bug.Nombre.ToUpper().Contains(filtros.Nombre.ToUpper()) &&
                                        bug.Estado == filtros.Estado &&
                                        bug.ProyectoId == filtros.ProyectoId);

            mockRepositorioBug.Setup(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()))
                .Returns(mapper.Map<List<Bug>>(mockResultado));

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester",
                Proyectos = new List<ProyectoDTO>(),
            };

            usuarioDTO.Proyectos.Add(new ProyectoDTO()
            {
                Id = 3,
                Nombre = "TestProyecto",
                Bugs = bugs,
            });

            void accion() => logicaBug.ObtenerBugs(filtros, usuarioDTO);

            Assert.ThrowsException<ExcepcionAccesoDenegado>(accion);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Never());
        }

        [TestMethod]
        public void TestObtenerListaBugsUsuarioSinProyectos()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester",
                Proyectos = new List<ProyectoDTO>(),
            };

            var resultado = logicaBug.ObtenerBugs(null, usuarioDTO);

            Assert.AreEqual(usuarioDTO.Proyectos.Count, resultado.Count);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Never());
        }

        [TestMethod]
        public void TestCrearBugSinPermisos()
        {
            usuarioDTO.Proyectos = new List<ProyectoDTO>();

            void accion() => logicaBug.CrearBug(bug, usuarioDTO);

            Assert.ThrowsException<ExcepcionAccesoDenegado>(accion);

            mockRepositorioBug.Verify(mock => mock.Crear(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestModificarBugSinPermisos()
        {
            usuarioDTO.Proyectos = new List<ProyectoDTO>();

            void accion() => logicaBug.ModificarBug(BUG_ID, bug, usuarioDTO);

            Assert.ThrowsException<ExcepcionAccesoDenegado>(accion);

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestModificarProyectoBugSinPermisos()
        {
            var bugDTO = new BugDTO()
            {
                ProyectoId = 140
            };


            var proyecto = new Proyecto()
            {
                Id = 140,
                Nombre = "ProyectoFake",
                Bugs = new List<Bug>()
            };

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(140))))
                .Returns(proyecto);

            void accion() => logicaBug.ModificarBug(BUG_ID, bugDTO, usuarioDTO);

            Assert.ThrowsException<ExcepcionAccesoDenegado>(accion);

            mockRepositorioBug.Verify(mock => mock.Modificar(It.IsAny<Bug>()), Times.Never());
        }

        [TestMethod]
        public void TestEliminarBugSinPermisos()
        {
            usuarioDTO.Proyectos = new List<ProyectoDTO>();

            void accion() => logicaBug.EliminarBug(BUG_ID, usuarioDTO);

            Assert.ThrowsException<ExcepcionAccesoDenegado>(accion);

            mockRepositorioBug.Verify(mock => mock.Eliminar(It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public void TestObtenerCantidadBugsPorUsuario()
        {
            var bugs = new List<BugDTO>();

            bugs.Add(new BugDTO() { Id = 1, Nombre = "Esta todo roto", Estado = "Resuelto", ProyectoId = 5 });
            bugs.Add(new BugDTO() { Id = 2, Nombre = "roto, nada funciona", Estado = "Resuelto", ProyectoId = 5, ResueltoPorId = 1, ResueltoPor = usuarioDTO });
            bugs.Add(new BugDTO() { Id = 3, Nombre = "Very roto", Estado = "Resuelto", ProyectoId = 5, ResueltoPorId = 1, ResueltoPor = usuarioDTO });

            var mockResultado = bugs.Where(b => b.ResueltoPorId == usuarioDTO.Id && b.Estado == "Resuelto");

            mockRepositorioBug.Setup(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()))
                .Returns(mapper.Map<List<Bug>>(mockResultado));

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(usuarioDTO));

            var resultado = logicaBug.ObtenerCantidadDeBugsPorUsuario(usuarioDTO.CorreoElectronico);

            Assert.AreEqual(2, resultado);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Once());
        }

        [TestMethod]
        public void TestObtenerBug()
        {
            var filtros = new FiltroDTO()
            {
                Id = BUG_ID
            };

            var proyectos = new List<ProyectoDTO>();

            var bugs = new List<BugDTO>();

            bugs.Add(bug);

            proyectos.Add(new ProyectoDTO()
            {
                Id = 1,
                Nombre = "TestProyecto",
                Bugs = bugs,
            });

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester",
                Proyectos = proyectos
            };

            mockRepositorioBug.Setup(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()))
                .Returns(mapper.Map<List<Bug>>(bugs));

            var resultado = logicaBug.ObtenerBug(BUG_ID, usuarioDTO);

            Assert.IsNotNull(resultado);

            mockRepositorioBug.Verify(mock => mock.Obtener(It.IsAny<Expression<Func<Bug, bool>>>()), Times.Once());
        }

        private void ValidarListaBugs(List<BugDTO> expectativa, List<BugDTO> resultado)
        {
            foreach (var (bug, indice) in expectativa.Select((valor, i) => (valor, i)))
            {
                Assert.AreEqual(bug.Nombre, resultado[indice].Nombre);
                Assert.AreEqual(bug.Descripcion, resultado[indice].Descripcion);
                Assert.AreEqual(bug.Estado, resultado[indice].Estado);
                Assert.AreEqual(bug.Version, resultado[indice].Version);
                Assert.AreEqual(bug.Id, resultado[indice].Id);
            }
        }
    }
}
