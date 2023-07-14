using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jiru.Dominio;
using Jiru.AccesoADatosTest;
using Jiru.AccesoADatos.Repositorios;
using System.Linq;

namespace Jiru.AccesoADatosTest
{
    [TestClass]
    public class RepositorioBugTest : RepositorioBaseTest
    {
        RepositorioBug repositorioBug;

        private readonly int ID_EXISTENTE = 1;

        [TestInitialize]
        public void Inicializar()
        {
            LevantarBase();

            repositorioBug = new RepositorioBug(DBContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LimpiarBase();
            repositorioBug = null;
        }

        [TestMethod]
        public void TestCrearBug()
        {
            Bug bug = new Bug()
            {
                Nombre = "No carga la pagina de Haaland",
                ProyectoId = 1,
                Version = "0.1.0",
                Descripcion = "Erling me reporto que no le carga la pagina.",
                Estado = Estado.Activo,
                ReportadoPorId = 1,
                ResueltoPorId = null
            };

            repositorioBug.Crear(bug);

            var bugsCreados = repositorioBug.Obtener(b => b.Nombre == bug.Nombre);

            Assert.AreEqual(1, bugsCreados.Count);
        }

        [TestMethod]
        public void TestObtenerBugExistente()
        {
            var bugCreado = repositorioBug.Obtener(ID_EXISTENTE);

            Assert.IsNotNull(bugCreado);
        }

        [TestMethod]
        public void TestObtenerBugsExistentes()
        {
            var bugsCreados = repositorioBug.Obtener();

            Assert.AreEqual(3, bugsCreados.Count());
        }

        [TestMethod]
        public void TestEliminarBugExistente()
        {
            repositorioBug.Eliminar(ID_EXISTENTE);

            var bugEliminado = repositorioBug.Obtener(ID_EXISTENTE);

            Assert.IsNull(bugEliminado);
        }

        [TestMethod]
        public void TestModificarBug()
        {
            var bugExistente = repositorioBug.Obtener(ID_EXISTENTE);

            bugExistente.Nombre = "Color incorrecto en el header";

            bugExistente.ResueltoPor = new Usuario()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "dev.ricky@gmailcom",
                Rol = Rol.Desarrollador
            };

            repositorioBug.Modificar(bugExistente);

            var bugModificado = repositorioBug.Obtener(ID_EXISTENTE);

            Assert.IsNotNull(bugModificado);
        }
    }
}
