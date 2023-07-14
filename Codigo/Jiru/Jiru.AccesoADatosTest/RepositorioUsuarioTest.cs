using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jiru.Dominio;
using Jiru.AccesoADatosTest;
using Jiru.AccesoADatos.Repositorios;

namespace Jiru.AccesoADatosTest
{
    [TestClass]
    public class RepositorioUsuarioTest : RepositorioBaseTest
    {
        RepositorioUsuario repositorioUsuario;

        private readonly int ID_EXISTENTE = 1;

        private readonly string TESTER_EXISTENTE = "el.ricky@gmailcom";

        private readonly string DEV_EXISTENTE = "dev.ricky@gmailcom";

        [TestInitialize]
        public void Inicializar()
        {
            LevantarBase();

            repositorioUsuario = new RepositorioUsuario(DBContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LimpiarBase();
            repositorioUsuario = null;
        }

        [TestMethod]
        public void TestCrearUsuario()
        {
            var usuario = new Usuario()
            {
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "ricky.fort@gmail.com",
                Rol = Rol.Administrador
            };

            repositorioUsuario.Crear(usuario);

            var usuarioCreado = repositorioUsuario.Obtener(usuario.CorreoElectronico);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerUsuarioExistenteConSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.Obtener(ID_EXISTENTE, true);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerUsuarioExistenteSinSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.Obtener(ID_EXISTENTE, false);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerDesarrolladorExistenteConSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.ObtenerDesarrollador(DEV_EXISTENTE, true);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerDesarrolladorExistenteSinSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.ObtenerDesarrollador(DEV_EXISTENTE, false);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerTesterExistenteConSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.ObtenerTester(TESTER_EXISTENTE, true);

            Assert.IsNotNull(usuarioCreado);
        }

        [TestMethod]
        public void TestObtenerTesterExistenteSinSeguimiento()
        {
            var usuarioCreado = repositorioUsuario.ObtenerTester(TESTER_EXISTENTE, false);

            Assert.IsNotNull(usuarioCreado);
        }
    }
}
