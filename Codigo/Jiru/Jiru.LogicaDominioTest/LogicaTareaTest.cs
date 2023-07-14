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
    public class LogicaTareaTest
    {
        private readonly int TAREA_ID = 1;

        private readonly int PROYECTO_ID = 1;

        ProyectoDTO proyecto;

        LogicaTarea logicaTarea;

        Mock<IRepositorioTarea> mockRepositorioTarea;

        Mock<IRepositorioProyecto> mockRepositorioProyecto;

        IMapper mapper;

        [TestInitialize]
        public void Inicializar()
        {

            proyecto = new ProyectoDTO()
            {
                Id = 1,
                Nombre = "YouTube",
                Bugs = new List<BugDTO>(),
                Testers = new List<UsuarioDTO>(),
                Desarrolladores = new List<UsuarioDTO>(),
                Tareas = new List<TareaDTO>(),
            };

            var perfilAutoMapper = new PerfilAutoMapper();

            var configuracionMapper = new MapperConfiguration(cfg => cfg.AddProfile(perfilAutoMapper));

            mapper = new Mapper(configuracionMapper);

            mockRepositorioTarea = new Mock<IRepositorioTarea>();

            mockRepositorioProyecto = new Mock<IRepositorioProyecto>();

            Proyecto retornarProyecto()
            {

                var p = mapper.Map<Proyecto>(proyecto);

                p.Id = PROYECTO_ID;

                return p;
            }

            mockRepositorioProyecto.Setup(mock => mock.Obtener(It.Is<int>(m => m.Equals(PROYECTO_ID))))
                .Returns(retornarProyecto);

            mockRepositorioTarea.Setup(mock => mock.Crear(It.IsAny<Tarea>()));

            logicaTarea = new LogicaTarea(mockRepositorioTarea.Object, mockRepositorioProyecto.Object, mapper);
        }


        [TestMethod]
        public void TestCrearTarea()
        {
            TareaDTO tarea = new TareaDTO()
            {
                Id = TAREA_ID,
                Nombre = "Error BD",
                ProyectoId = 1,
                CostoPorHora = 250,
                DuracionHoras = 2
            };

            logicaTarea.CrearTarea(tarea);

            mockRepositorioTarea.Verify(mock => mock.Crear(It.IsAny<Tarea>()), Times.Once());
        }

        [TestMethod]
        public void TestCrearTareaProyectoInexistente()
        {
            TareaDTO tarea = new TareaDTO()
            {
                Id = TAREA_ID,
                Nombre = "Error BD",
                ProyectoId = 3,
                CostoPorHora = 250,
                DuracionHoras = 2
            };

            void accion() => logicaTarea.CrearTarea(tarea);

            Assert.ThrowsException<ExcepcionProyectoInexistente>(accion);

            mockRepositorioTarea.Verify(mock => mock.Crear(It.IsAny<Tarea>()), Times.Never());
        }

        [TestMethod]
        public void TestObtenerTareas()
        {
            List<TareaDTO> tareas = new List<TareaDTO>();

            tareas.Add(new TareaDTO()
            {
                Nombre = "Error en el login",
                CostoPorHora = 124,
                DuracionHoras = 200,
                Id = 1,
                ProyectoId = 2
            });

            tareas.Add(new TareaDTO()
            {
                Nombre = "Error en la vista principal",
                CostoPorHora = 300,
                DuracionHoras = 100,
                Id = 2,
                ProyectoId = 2
            });

            tareas.Add(new TareaDTO()
            {
                Nombre = "Arreglar compu jefe",
                CostoPorHora = 2000,
                DuracionHoras = 10,
                Id = 3,
                ProyectoId = 2
            });

            List<ProyectoDTO> proyectos = new List<ProyectoDTO>();

            proyectos.Add(new ProyectoDTO()
            {
                Nombre = "MercadoLibre",
                Id = 2,
                Tareas = tareas
            });

            
            mockRepositorioTarea.Setup(mock => mock.Obtener())
                .Returns(() => mapper.Map<List<Tarea>>(tareas));

            var resultado = logicaTarea.ObtenerTareas(new UsuarioDTO() { Rol = "Desarrollador", Proyectos = proyectos });

            Assert.AreEqual(tareas.Count, resultado.Count);
        }
    }
}
