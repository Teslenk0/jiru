
using AutoMapper;
using Jiru.Configuracion;
using Jiru.Dominio;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IAccesoADatos;
using Jiru.LogicaDominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Jiru.LogicaDominioTest
{
    [TestClass]
    public class LogicaProyectoTest
    {
        private readonly int PROYECTO_ID = 1;

        ProyectoDTO proyecto;

        LogicaProyecto logicaProyecto;

        Mock<IRepositorioProyecto> mockRepositorioProyecto;

        Mock<IRepositorioUsuario> mockRepositorioUsuario;

        IMapper mapper;

        UsuarioDTO usuario;

        [TestInitialize]
        public void Inicializar()
        {
            proyecto = new ProyectoDTO()
            {
                Nombre = "YouTube",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>()

            };

            var perfilAutoMapper = new PerfilAutoMapper();

            var configuracionMapper = new MapperConfiguration(cfg => cfg.AddProfile(perfilAutoMapper));

            mapper = new Mapper(configuracionMapper);

            mockRepositorioProyecto = new Mock<IRepositorioProyecto>();

            Proyecto retornarProyecto()
            {

                var p = mapper.Map<Proyecto>(proyecto);

                p.Id = PROYECTO_ID;

                return p;
            }

            usuario = new UsuarioDTO()
            {
                Id = 1,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                CostoPorHora = 250
            };

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(PROYECTO_ID))))
                .Returns(retornarProyecto);

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(proyecto.Nombre))))
                .Returns(retornarProyecto);

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => !m.Equals(PROYECTO_ID))))
                .Returns(() => null);

            mockRepositorioProyecto.Setup(mock => mock.Crear(It.IsAny<Proyecto>()));

            mockRepositorioProyecto.Setup(mock => mock.Eliminar(It.IsAny<int>()));

            mockRepositorioProyecto.Setup(mock => mock.Modificar(It.IsAny<Proyecto>()));

            mockRepositorioProyecto.Setup(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()));

            mockRepositorioProyecto.Setup(mock => mock.DesasignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()));

            mockRepositorioUsuario = new Mock<IRepositorioUsuario>();

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(usuario.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(usuario));

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(usuario.Id)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(usuario));

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<int>(m => !m.Equals(usuario.Id)), It.IsAny<bool>()))
                .Returns(() => null);

            logicaProyecto = new LogicaProyecto(mockRepositorioProyecto.Object, mapper, mockRepositorioUsuario.Object);
        }

        [TestMethod]
        public void TestCrearProyecto()
        {
            ProyectoDTO proyectoNuevo = new ProyectoDTO()
            {
                Nombre = "NewProject",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>(),
                Id = PROYECTO_ID
            };

            Proyecto retornarProyectoNuevo()
            {
                var p = mapper.Map<Proyecto>(proyectoNuevo);

                p.Id = PROYECTO_ID;

                return p;
            }

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(PROYECTO_ID))))
                .Returns(retornarProyectoNuevo);

            logicaProyecto.CrearProyecto(proyectoNuevo);

            var resultado = logicaProyecto.ObtenerProyecto(proyectoNuevo.Id);

            ValidarCamposComunes(proyectoNuevo, resultado);

            mockRepositorioProyecto.Verify(mock => mock.Crear(It.IsAny<Proyecto>()), Times.Once());
        }

        [TestMethod]
        public void TestImportarProyecto()
        {
            ProyectoDTO proyectoNuevo = new ProyectoDTO()
            {
                Nombre = "NewProject",
                Bugs = new List<BugDTO>() { 
                    new BugDTO()
                    {
                        Nombre = "Error BD",
                        Descripcion = "No carga BD",
                        Version = "Primera",
                        Estado = "Activo",
                        DuracionHoras = 6
                    },
                    new BugDTO()
                    {
                        Nombre = "Error Servidor",
                        Descripcion = "No carga Servidor",
                        Version = "Segunda",
                        Estado = "Resuelto",
                        DuracionHoras = 3
                    }
                },
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>(),
                Id = PROYECTO_ID
            };

            Proyecto retornarProyectoNuevo()
            {
                var p = mapper.Map<Proyecto>(proyectoNuevo);

                p.Id = PROYECTO_ID;

                return p;
            }

            var desarrollador = new Desarrollador()
            {
                Nombre = "Erling",
                CorreoElectronico = "erling@borussia.com",
                Apellido = "Haaland",
                Id = 1,
                CostoPorHora = 200,
                NombreUsuario = "ERLINGBOR",
                Rol = Rol.Desarrollador
            };

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(PROYECTO_ID))))
                .Returns(retornarProyectoNuevo);

            mockRepositorioProyecto.SetupSequence(mock => mock.Obtener(It.Is<string>(m => m.Equals(proyectoNuevo.Nombre))))
               .Returns(() => null)
               .Returns(retornarProyectoNuevo);

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(desarrollador.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(desarrollador));

            mockRepositorioUsuario.Setup(mock => mock.ObtenerDesarrollador(It.Is<string>(m => m.Equals(desarrollador.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Desarrollador>(desarrollador));

            logicaProyecto.ImportarProyecto(proyectoNuevo, usuario, desarrollador.CorreoElectronico);


            mockRepositorioProyecto.Verify(mock => mock.Crear(It.IsAny<Proyecto>()), Times.Once());

            mockRepositorioProyecto.Verify(mock => mock.Modificar(It.IsAny<Proyecto>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearProyectoNombreExistente()
        {
            void accion() => logicaProyecto.CrearProyecto(proyecto);

            Assert.ThrowsException<ExcepcionProyectoYaExistente>(accion);

            mockRepositorioProyecto.Verify(mock => mock.Crear(It.IsAny<Proyecto>()), Times.Never());
        }

        [TestMethod]
        public void TestEliminarProyectoExistente()
        {
            logicaProyecto.EliminarProyecto(PROYECTO_ID);

            mockRepositorioProyecto.Verify(r => r.Eliminar(PROYECTO_ID), Times.Once);
        }

        [TestMethod]
        public void TestEliminarProyectoInexistente()
        {
            void accion() => logicaProyecto.EliminarProyecto(123);

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);
        }

        [TestMethod]
        public void TestModificarProyectoExistente()
        {
            proyecto.Nombre = "Global Sports Platform";

            proyecto.Desarrolladores.Add(new UsuarioDTO()
            {
                Id = 1,
                Nombre = "Nikola",
                Apellido = "Tesla",
                CorreoElectronico = "ntesla@tesla.com",
                Contrasena = "El0nPlsPayCopyright5S2",
                NombreUsuario = "T35L4",
                Rol = "Desarrollador"
            });

            proyecto.Testers.Add(new UsuarioDTO()
            {
                Id = 2,
                Nombre = "Carlos",
                Apellido = "Tevez",
                CorreoElectronico = "carlos.tevez@bocajuniors.com.ar",
                NombreUsuario = "Apache",
                Contrasena = "VeryDificulContra",
                Rol = "Tester"
            });

            proyecto.Bugs.Add(new BugDTO()
            {
                Id = 1,
                Estado = "Activo",
                Descripcion = "NO ME CARGA LA PAGINAAAAA!!!!!!!!! RESOLVER PLS - PRIORIDAD 845 REALLY HIGH. Reporter: Apache",
                Nombre = "BOEL-1: No carga la pagina",
                Version = "1.0.0"
            });

            logicaProyecto.ModificarProyecto(PROYECTO_ID, proyecto);

            var resultado = logicaProyecto.ObtenerProyecto(PROYECTO_ID);

            ValidarCamposComunes(proyecto, resultado);

            mockRepositorioProyecto.Verify(mock => mock.Modificar(It.IsAny<Proyecto>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignarDesarrollador()
        {
            var u = new UsuarioDTO()
            {
                Id = 2,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Desarrollador"
            };

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(u.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(u));

            logicaProyecto.AsignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Desarrollador");

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignarDesarrolladorYaExistente()
        {
            var u = new UsuarioDTO()
            {
                Id = 2,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Desarrollador"
            };

            proyecto.Desarrolladores.Add(u);

            void accion () => logicaProyecto.AsignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Desarrollador");

            Assert.ThrowsException<ExcepcionUsuarioYaAsignado>(accion);

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestAsignarTesterYaExistente()
        {
            var u = new UsuarioDTO()
            {
                Id = 2,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester"
            };

            proyecto.Testers.Add(u);

            void accion() => logicaProyecto.AsignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Tester");

            Assert.ThrowsException<ExcepcionUsuarioYaAsignado>(accion);

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestDesasignarDesarrolladorNoAsignado()
        {
            var u = new UsuarioDTO()
            {
                Id = 3,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Desarrollador"
            };

            void accion() => logicaProyecto.DesasignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Desarrollador");

            Assert.ThrowsException<ExcepcionUsuarioNoAsignado>(accion);

            mockRepositorioProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestDesasignarTesterNoAsignado()
        {
            var u = new UsuarioDTO()
            {
                Id = 3,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester"
            };

            void accion() => logicaProyecto.DesasignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Tester");

            Assert.ThrowsException<ExcepcionUsuarioNoAsignado>(accion);

            mockRepositorioProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestDesasignarDesarrollador()
        {
            var u = new UsuarioDTO()
            {
                Id = 2,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Desarrollador"
            };

            proyecto.Desarrolladores.Add(u);

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(u.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(u));

            logicaProyecto.DesasignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Desarrollador");

            mockRepositorioProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignarTester()
        {
            var u = new UsuarioDTO()
            {
                Id = 3,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester"
            };

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(u.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(u));

            logicaProyecto.AsignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Tester");

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestDesasignarTester()
        {
            var u = new UsuarioDTO()
            {
                Id = 3,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = "Tester"
            };

            proyecto.Testers.Add(u);

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(u.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(u));

            logicaProyecto.DesasignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Tester");

            mockRepositorioProyecto.Verify(mock => mock.DesasignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void TestAsignarUsuarioNoExistente()
        {
            void accion() => logicaProyecto.AsignarUsuario(PROYECTO_ID, "teslasapbe@gmail.net", "Desarrollador");

            Assert.ThrowsException<ExcepcionUsuarioInexistente>(accion);

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestAsignarUsuarioProyectoNoExistente()
        {
            void accion() => logicaProyecto.AsignarUsuario(124, usuario.CorreoElectronico, "Desarrollador");

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void TestAsignarUsuarioRolNoCoincide()
        {
            var u = new UsuarioDTO()
            {
                Id = 3,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "teslasapbe@gmail.com.br",
                Rol = "Tester"
            };

            proyecto.Testers.Add(u);

            mockRepositorioUsuario.Setup(mock => mock.Obtener(It.Is<string>(m => m.Equals(u.CorreoElectronico)), It.IsAny<bool>()))
                .Returns(mapper.Map<Usuario>(u));

            void accion() => logicaProyecto.AsignarUsuario(PROYECTO_ID, u.CorreoElectronico, "Desarrollador");

            Assert.ThrowsException<ExcepcionDatosIncorrectos>(accion);

            mockRepositorioProyecto.Verify(mock => mock.AsignarUsuario(It.IsAny<Proyecto>(), It.IsAny<Usuario>(), It.IsAny<string>()), Times.Never());
        }


        [TestMethod]
        public void TestModificarProyectoInexistente()
        {
            void accion() => logicaProyecto.ModificarProyecto(123, null);

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);
        }

        [TestMethod]
        public void TestObtenerProyectoPorNombre()
        {
            var resultado = logicaProyecto.ObtenerProyecto(proyecto.Nombre);

            ValidarCamposComunes(proyecto, resultado);
        }

        [TestMethod]
        public void TestObtenerCantidadDeBugsPorProyectoMasCostos()
        {
            List<BugDTO> bugs1 = new List<BugDTO>();

            bugs1.Add(new BugDTO()
            {
                Nombre = "Error en el login",
                Descripcion = "Increible error en el login no me deja entrar",
                Estado = "Activo",
                Id = 1,
                Version = "1.0.0",
                DuracionHoras = 6
            });
            bugs1.Add(new BugDTO()
            {
                Nombre = "Error en la pantalla principal",
                Descripcion = "No veo nada",
                Estado = "Activo",
                Id = 2,
                Version = "1.0.0",
                DuracionHoras = 6
            });
            bugs1.Add(new BugDTO()
            {
                Nombre = "Terraform fallando al intentar aplicar cambios",
                Descripcion = "Terraform falla a la hora de aplicar los cambios",
                Estado = "Resuelto",
                Id = 3,
                Version = "2.0.0",
                DuracionHoras = 6,
                ResueltoPor = usuario
            });

            List<BugDTO> bugs2 = new List<BugDTO>();

            bugs2.Add(new BugDTO()
            {
                Nombre = "",
                Descripcion = "API no funciona",
                Estado = "Resuelto",
                Id = 1,
                Version = "1.0.0",
                DuracionHoras = 6,
                ResueltoPor = usuario
            });
            bugs2.Add(new BugDTO()
            {
                Nombre = "No hay estabilidad en el sistema",
                Descripcion = "Se cae cada dos por tres. Saludos",
                Estado = "Resuelto",
                Id = 3,
                Version = "2.0.0",
                DuracionHoras = 6,
                ResueltoPor = usuario
            });

            List<TareaDTO> tareas = new List<TareaDTO>();

            tareas.Add(new TareaDTO()
            {
                Nombre = "Se rompio el theme principal",
                Id = 1,
                DuracionHoras = 6,
                CostoPorHora = 650
            });

            tareas.Add(new TareaDTO()
            {
                Nombre = "Arreglar auriculares de Belen",
                Id = 2,
                DuracionHoras = 14,
                CostoPorHora = 0
            });

            List<UsuarioDTO> usuarioDTOs = new List<UsuarioDTO>();

            usuarioDTOs.Add(usuario);

            List<ProyectoDTO> proyectos = new List<ProyectoDTO>();

            proyectos.Add(proyecto);

            proyectos.Add(new ProyectoDTO()
            {
                Nombre = "MercadoLibre",
                Id = 2,
                Bugs = bugs1,
                Tareas = new List<TareaDTO>()
            });

            proyectos.Add(new ProyectoDTO()
            {
                Nombre = "Jiru",
                Id = 3,
                Bugs = bugs2,
                Tareas = tareas
            });

            mockRepositorioProyecto.Setup(mock => mock.Obtener())
                .Returns(() => mapper.Map<List<Proyecto>>(proyectos));

            var resultado = logicaProyecto.ObtenerProyectos(new UsuarioDTO() { Rol = "Administrador" });

            ValidarCantBugs(proyectos, resultado);
        }

        [TestMethod]
        public void TestObtenerProyectos()
        {
            List<BugDTO> bugs1 = new List<BugDTO>();

            bugs1.Add(new BugDTO()
            {
                Nombre = "Error en el login",
                Descripcion = "Increible error en el login no me deja entrar",
                Estado = "Activo",
                Id = 1,
                Version = "1.0.0"
            });
            bugs1.Add(new BugDTO()
            {
                Nombre = "Error en la pantalla principal",
                Descripcion = "No veo nada",
                Estado = "Activo",
                Id = 2,
                Version = "1.0.0"
            });
            bugs1.Add(new BugDTO()
            {
                Nombre = "Terraform fallando al intentar aplicar cambios",
                Descripcion = "Terraform falla a la hora de aplicar los cambios",
                Estado = "Resuelto",
                Id = 3,
                Version = "2.0.0"
            });

            List<BugDTO> bugs2 = new List<BugDTO>();

            bugs2.Add(new BugDTO()
            {
                Nombre = "",
                Descripcion = "API no funciona",
                Estado = "Resuelto",
                Id = 1,
                Version = "1.0.0"
            });
            bugs2.Add(new BugDTO()
            {
                Nombre = "No hay estabilidad en el sistema",
                Descripcion = "Se cae cada dos por tres. Saludos",
                Estado = "Resuelto",
                Id = 3,
                Version = "2.0.0"
            });

            List<ProyectoDTO> proyectos = new List<ProyectoDTO>();

            proyectos.Add(proyecto);

            proyectos.Add(new ProyectoDTO()
            {
                Nombre = "MercadoLibre",
                Id = 2,
                Bugs = bugs1
            });

            proyectos.Add(new ProyectoDTO()
            {
                Nombre = "Jiru",
                Id = 3,
                Bugs = bugs2
            });

            mockRepositorioProyecto.Setup(mock => mock.Obtener())
                .Returns(() => mapper.Map<List<Proyecto>>(proyectos));

            var resultado = logicaProyecto.ObtenerProyectos(new UsuarioDTO() { Rol = "Desarrollador", Proyectos = proyectos });

            Assert.AreEqual(proyectos.Count, resultado.Count);
        }

        private void ValidarCamposComunes(ProyectoDTO expectativa, ProyectoDTO resultado)
        {
            Assert.AreEqual<int>(PROYECTO_ID, resultado.Id);
            Assert.AreEqual(expectativa.Nombre, resultado.Nombre);

            ValidarListaBugs(expectativa.Bugs, resultado.Bugs);
            ValidarListaUsuarios(expectativa.Desarrolladores, resultado.Desarrolladores);
            ValidarListaUsuarios(expectativa.Testers, resultado.Testers);

        }

        private void ValidarCantBugs(List<ProyectoDTO> expectativa, List<ProyectoDTO> resultado)
        {
            foreach (var (proyecto, indice) in expectativa.Select((valor, i) => (valor, i)))
            {
                Assert.AreEqual(proyecto.Bugs.Count, resultado[indice].CantBugs);
            }
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

        private void ValidarListaUsuarios(List<UsuarioDTO> expectativa, List<UsuarioDTO> resultado)
        {
            foreach (var (usuario, indice) in expectativa.Select((valor, i) => (valor, i)))
            {
                Assert.AreEqual(usuario.Nombre, resultado[indice].Nombre);
                Assert.AreEqual(usuario.Apellido, resultado[indice].Apellido);
                Assert.AreEqual(usuario.CorreoElectronico, resultado[indice].CorreoElectronico);
                Assert.AreEqual(usuario.Rol, resultado[indice].Rol);
                Assert.AreEqual(usuario.NombreUsuario, resultado[indice].NombreUsuario);
                Assert.AreEqual(usuario.Id, resultado[indice].Id);
            }
        }
    }
}
