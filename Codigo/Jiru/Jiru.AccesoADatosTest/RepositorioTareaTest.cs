using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jiru.Dominio;
using Jiru.AccesoADatos.Repositorios;
using System.Linq;

namespace Jiru.AccesoADatosTest
{
    [TestClass]
    public class RepositorioTareaTest : RepositorioBaseTest
    {
        RepositorioTarea repositorioTarea;

        [TestInitialize]
        public void Inicializar()
        {
            LevantarBase();

            repositorioTarea = new RepositorioTarea(DBContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LimpiarBase();
            repositorioTarea = null;
        }

        [TestMethod]
        public void TestCrearTarea()
        {
            Tarea tarea = new Tarea()
            {
                Nombre = "No carga la pagina de Haaland",
                ProyectoId = 1,
                CostoPorHora = 250,
                DuracionHoras = 2
            };

            repositorioTarea.Crear(tarea);
      
            var tareasCreadas = repositorioTarea.Obtener();

            Assert.AreEqual(1, tareasCreadas.Count);
        }
    }
}
