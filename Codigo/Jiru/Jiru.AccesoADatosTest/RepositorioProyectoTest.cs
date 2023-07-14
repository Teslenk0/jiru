using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jiru.Dominio;
using Jiru.AccesoADatosTest;
using Jiru.AccesoADatos.Repositorios;
using System.Collections.Generic;
using System.Linq;

namespace Jiru.AccesoADatosTest
{
    [TestClass]
    public class RepositorioProyectoTest : RepositorioBaseTest
    {
        RepositorioProyecto repositorioProyecto;

        RepositorioUsuario repositorioUsuario;

        private readonly int ID_EXISTENTE = 1;

        private readonly int ID_TESTER_EXISTENTE = 3;

        private readonly int ID_DEV_EXISTENTE = 2;

        [TestInitialize]
        public void Inicializar()
        {
            LevantarBase();

            repositorioProyecto = new RepositorioProyecto(DBContext);

            repositorioUsuario = new RepositorioUsuario(DBContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LimpiarBase();
            repositorioProyecto = null;
        }

        [TestMethod]
        public void TestCrearProyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "erling-halaand.com",
                Bugs = new List<Bug>(),
                Desarrolladores = new List<Desarrollador>(),
                Testers = new List<Tester>()
            };

            repositorioProyecto.Crear(proyecto);

            var proyectoCreado = repositorioProyecto.Obtener(proyecto.Nombre);

            Assert.IsNotNull(proyectoCreado);
        }

        [TestMethod]
        public void TestObtenerProyectoExistente()
        {
            var proyectoCreado = repositorioProyecto.Obtener(ID_EXISTENTE);

            Assert.IsNotNull(proyectoCreado);
        }

        [TestMethod]
        public void TestObtenerProyectosExistentes()
        {
            var proyectoCreado = repositorioProyecto.Obtener();

            Assert.AreEqual(2, proyectoCreado.Count);
        }

        [TestMethod]
        public void TestEliminarProyectoExistente()
        {
            repositorioProyecto.Eliminar(ID_EXISTENTE);

            var proyectoEliminado = repositorioProyecto.Obtener(ID_EXISTENTE);

            Assert.IsNull(proyectoEliminado);
        }

        [TestMethod]
        public void TestModificarProyecto()
        {
            var proyectoExistente = repositorioProyecto.Obtener(ID_EXISTENTE);

            proyectoExistente.Nombre = "NombreActualizado";

            proyectoExistente.Desarrolladores.Add(new Desarrollador()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "dev.ricky@gmailcom",
                Rol = Rol.Desarrollador
            });

            repositorioProyecto.Modificar(proyectoExistente);

            var proyectoModificado = repositorioProyecto.Obtener(ID_EXISTENTE);

            Assert.IsNotNull(proyectoModificado);
        }

        [TestMethod]
        public void TestAsignarDesarrolladorAProyecto()
        {
            var proyectoExistente = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuarioExistente = new Usuario()
            {
                Id = ID_DEV_EXISTENTE
            };

            repositorioProyecto.AsignarUsuario(proyectoExistente, usuarioExistente, "Desarrollador");

            var proyectoModificado = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuario = proyectoModificado.Desarrolladores.Where(dev => dev.Id == usuarioExistente.Id).FirstOrDefault();

            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void TestAsignarTesterAProyecto()
        {
            var proyectoExistente = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuarioExistente = new Usuario()
            {
                Id = ID_TESTER_EXISTENTE
            };

            repositorioProyecto.AsignarUsuario(proyectoExistente, usuarioExistente, "Tester");

            var proyectoModificado = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuario = proyectoModificado.Testers.Where(dev => dev.Id == usuarioExistente.Id).FirstOrDefault();

            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void TestDesasignarDesarrolladorDeProyecto()
        {
            var proyectoExistente = repositorioProyecto.Obtener(ID_EXISTENTE);

            Usuario dev = new Usuario()
            {   
                Id = ID_DEV_EXISTENTE
            };

            repositorioProyecto.DesasignarUsuario(proyectoExistente, dev, "Desarrollador");

            var proyectoModificado = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuario = proyectoModificado.Desarrolladores.Where(usuario => usuario.Id == dev.Id).FirstOrDefault();

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void TestDesasignarTesterDeProyecto()
        {
            var proyectoExistente = repositorioProyecto.Obtener(ID_EXISTENTE);

            Usuario tester = new Usuario()
            {
                Id = ID_TESTER_EXISTENTE
            };

            repositorioProyecto.DesasignarUsuario(proyectoExistente, tester, "Tester");

            var proyectoModificado = repositorioProyecto.Obtener(ID_EXISTENTE);

            var usuario = proyectoModificado.Testers.Where(usuario => usuario.Id == tester.Id).FirstOrDefault();

            Assert.IsNull(usuario);
        }
    }
}
