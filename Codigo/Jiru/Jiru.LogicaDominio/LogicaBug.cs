using AutoMapper;
using Jiru.Dominio;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using Jiru.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jiru.LogicaDominio
{
    public class LogicaBug : ILogicaBug
    {
        private readonly IMapper _mapper;

        private readonly IRepositorioBug _repositorioBug;

        private readonly IRepositorioProyecto _repositorioProyecto;

        private readonly IRepositorioUsuario _repositorioUsuario;


        public LogicaBug(IRepositorioBug repositorioBug, IRepositorioProyecto repositorioProyecto, IMapper mapper, IRepositorioUsuario repositorioUsuario)
        {
            this._repositorioBug = repositorioBug;
            this._repositorioProyecto = repositorioProyecto;
            this._repositorioUsuario = repositorioUsuario;
            this._mapper = mapper;
        }

        public void CrearBug(BugDTO bug, UsuarioDTO usuarioDTO)
        {
            Bug b = _mapper.Map<Bug>(bug);

            var proyecto = _repositorioProyecto.Obtener(b.ProyectoId);

            if (proyecto == null)
            {
                throw new ExcepcionProyectoInexistente(b.ProyectoId);
            }

            if (usuarioDTO.Rol != Rol.Administrador.ToString() && !usuarioDTO.Proyectos.Exists(p => p.Id == b.ProyectoId))
            {
                throw new ExcepcionAccesoDenegado();
            }

            var reportadoPor = _repositorioUsuario.Obtener(b.ReportadoPorId);

            if (reportadoPor == null)
            {
                throw new ExcepcionUsuarioInexistente(b.ReportadoPorId);
            }

            b.ResueltoPorId = null;

            b.ResueltoPor = null;

            _repositorioBug.Crear(b);
        }

        public void EliminarBug(int id, UsuarioDTO usuarioDTO)
        {
            var b = _repositorioBug.Obtener(id);

            if (b == null)
            {
                throw new ExcepcionBugInexistente(id);
            }

            if (usuarioDTO.Rol != Rol.Administrador.ToString() && !usuarioDTO.Proyectos.Exists(p => p.Id == b.ProyectoId))
            {
                throw new ExcepcionAccesoDenegado();
            }


            _repositorioBug.Eliminar(id);

        }

        public void ModificarBug(int id, BugDTO bugDTO, UsuarioDTO usuarioDTO)
        {
            var b = _repositorioBug.Obtener(id);

            if (b == null)
            {
                throw new ExcepcionBugInexistente(id);
            }

            if (usuarioDTO.Rol != Rol.Administrador.ToString() && !usuarioDTO.Proyectos.Exists(p => p.Id == b.ProyectoId))
            {
                throw new ExcepcionAccesoDenegado();
            }

            var bug = _mapper.Map<Bug>(bugDTO);

            if (usuarioDTO.Rol != Rol.Desarrollador.ToString())
            {
                if (bugDTO.ProyectoId != b.ProyectoId)
                {
                    var proyecto = _repositorioProyecto.Obtener(bugDTO.ProyectoId);

                    if (proyecto == null)
                    {
                        throw new ExcepcionProyectoInexistente(bugDTO.ProyectoId);
                    }

                    if (usuarioDTO.Rol != Rol.Administrador.ToString() && !usuarioDTO.Proyectos.Exists(p => p.Id == bugDTO.ProyectoId))
                    {
                        throw new ExcepcionAccesoDenegado();
                    }

                    b.Proyecto = proyecto;
                }

                b.Nombre = bug.Nombre;
                b.Descripcion = bug.Descripcion;
                b.Version = bug.Version;
            }

            if (b.Estado != bug.Estado)
            {

                if (bug.Estado == Estado.Resuelto)
                {

                    var resueltoPor = _repositorioUsuario.Obtener(bug.ResueltoPorId);

                    if (resueltoPor == null)
                    {
                        throw new ExcepcionUsuarioInexistente(bug.ResueltoPorId);
                    }

                    b.DuracionHoras = bug.DuracionHoras;
                    b.ResueltoPorId = bug.ResueltoPorId;
                }
                else
                {
                    b.ResueltoPor = null;
                }
                b.Estado = bug.Estado;
            }

            if (bug.DuracionHoras != b.DuracionHoras && b.Estado == Estado.Resuelto)
            {
                b.DuracionHoras = bug.DuracionHoras;
            }

            _repositorioBug.Modificar(b);

        }

        public BugDTO ObtenerBug(int id, UsuarioDTO usuarioDTO)
        {
            var resultado = ObtenerBugs(new FiltroDTO()
            {
                Id = id
            }, usuarioDTO);

            return _mapper.Map<BugDTO>(resultado.Where(b => b.Id == id).FirstOrDefault());
        }

        public List<BugDTO> ObtenerBugs(FiltroDTO filtros, UsuarioDTO usuarioDTO)
        {
            var consultaFiltros = PredicateBuilder.True<Bug>();

            if (usuarioDTO.Rol != Rol.Administrador.ToString())
            {
                if(usuarioDTO.Proyectos.Count < 1) 
                {
                    return new List<BugDTO>();
                } else
                {
                    var idProyectos = usuarioDTO.Proyectos.Select(proyecto => proyecto.Id);

                    consultaFiltros = consultaFiltros.And(bug => idProyectos.Contains(bug.ProyectoId));
                }
                
            }

            if (filtros == null || (Rol.Administrador.ToString() != usuarioDTO.Rol && Rol.Tester.ToString() != usuarioDTO.Rol))
            {
                return _mapper.Map<List<BugDTO>>(_repositorioBug.Obtener(consultaFiltros));
            }

            if (filtros.Id.ToString() != null && filtros.Id > 0)
            {
                consultaFiltros = consultaFiltros.And<Bug>(bug => bug.Id == filtros.Id);
            }

            if (filtros.ProyectoId.ToString() != null && filtros.ProyectoId > 0)
            {
                if(usuarioDTO.Rol != Rol.Administrador.ToString())
                {
                    var proyectos = usuarioDTO.Proyectos.Exists(p => p.Id == filtros.ProyectoId);

                    if (!proyectos)
                    {
                        throw new ExcepcionAccesoDenegado();
                    }
                }                

                consultaFiltros = consultaFiltros.And<Bug>(bug => bug.ProyectoId == filtros.ProyectoId);
            }

            if (filtros.Nombre != null)
            {
                consultaFiltros = consultaFiltros.And<Bug>(bug => bug.Nombre.ToUpper().Contains(filtros.Nombre.ToUpper()));
            }

            if (filtros.Estado != null)
            {
                Estado estado = (Estado)Enum.Parse(typeof(Estado), filtros.Estado);

                consultaFiltros = consultaFiltros.And<Bug>(bug => bug.Estado == estado);
            }

            var resultado = _repositorioBug.Obtener(consultaFiltros);

            return _mapper.Map<List<BugDTO>>(resultado);
        }

        public int ObtenerCantidadDeBugsPorUsuario(string correoElectronico)
        {
            var usuario = _repositorioUsuario.Obtener(correoElectronico, false);

            if(usuario == null)
            {
                throw new ExcepcionUsuarioInexistente(correoElectronico);
            }

            var consultaFiltros = PredicateBuilder.True<Bug>().And(b => b.ResueltoPorId == usuario.Id).And(b => b.Estado == Estado.Resuelto);

            return _repositorioBug.Obtener(consultaFiltros).Count;
        }
    }
}
