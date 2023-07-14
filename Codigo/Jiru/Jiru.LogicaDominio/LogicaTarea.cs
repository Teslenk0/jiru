using AutoMapper;
using Jiru.Dominio;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using System.Collections.Generic;
using System.Linq;

namespace Jiru.LogicaDominio
{
    public class LogicaTarea : ILogicaTarea
    {
        private readonly IMapper _mapper;

        private readonly IRepositorioTarea _repositorioTarea;

        private readonly IRepositorioProyecto _repositorioProyecto;


        public LogicaTarea(IRepositorioTarea repositorioTarea, IRepositorioProyecto repositorioProyecto, IMapper mapper)
        {
            this._repositorioTarea = repositorioTarea;
            this._repositorioProyecto = repositorioProyecto;
            this._mapper = mapper;
        }

        public void CrearTarea(TareaDTO tarea)
        {
            Tarea t = _mapper.Map<Tarea>(tarea);

            var proyecto = _repositorioProyecto.Obtener(t.ProyectoId);

            if (proyecto == null)
            {
                throw new ExcepcionProyectoInexistente(t.ProyectoId);
            }

            _repositorioTarea.Crear(t);
        }

        public List<TareaDTO> ObtenerTareas(UsuarioDTO usuarioDTO)
        {
            var tareas = _repositorioTarea.Obtener();

            if (usuarioDTO.Rol != Rol.Administrador.ToString())
            {
                var idProyectos = usuarioDTO.Proyectos.Select(proyecto => proyecto.Id);

                var resultado = _mapper.Map<List<TareaDTO>>(tareas.Where(tarea => idProyectos.Contains(tarea.ProyectoId)));

                resultado.Sort((p, q) => p.ProyectoId.CompareTo(q.ProyectoId));

                return resultado;
            }

            return _mapper.Map<List<TareaDTO>>(tareas);
        }

    }
}
