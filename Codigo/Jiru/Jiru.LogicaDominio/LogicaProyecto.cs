using AutoMapper;
using Jiru.Dominio;
using Jiru.DTOs;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using Jiru.Excepciones.Base;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Jiru.LogicaDominio
{
    public class LogicaProyecto : ILogicaProyecto
    {

        private readonly IRepositorioProyecto _repositorioProyecto;

        private readonly IRepositorioUsuario _repositorioUsuario;

        private readonly IMapper _mapper;

        public LogicaProyecto(IRepositorioProyecto repositorioProyecto, IMapper mapper, IRepositorioUsuario repositorioUsuario)
        {
            this._repositorioProyecto = repositorioProyecto;

            this._repositorioUsuario = repositorioUsuario;

            this._mapper = mapper;
        }

        public void CrearProyecto(ProyectoDTO proyecto)
        {
            Proyecto p = _mapper.Map<Proyecto>(proyecto);

            var proyectoExistente = _repositorioProyecto.Obtener(p.Nombre);

            if (proyectoExistente != null)
            {
                throw new ExcepcionProyectoYaExistente(p.Nombre);
            }

            _repositorioProyecto.Crear(p);
        }

        public void ImportarProyecto(ProyectoDTO proyecto, UsuarioDTO usuarioReportador, string correoUsuarioResuelve)
        {

            var usuarioResuelve = _repositorioUsuario.Obtener(correoUsuarioResuelve, false);

            if (usuarioResuelve == null)
            {
                throw new ExcepcionUsuarioInexistente(correoUsuarioResuelve);
            }

            if(usuarioResuelve.Rol != Rol.Desarrollador && usuarioResuelve.Rol != Rol.Tester)
            {
                throw new ExcepcionDatosIncorrectos($"El usuario debe ser desarrollador o tester. Rol actual: {usuarioResuelve.Rol.ToString()}");
            }

            if (proyecto.Bugs.Count > 0)
            {
                foreach (var bug in proyecto.Bugs)
                {
                    bug.ReportadoPorId = usuarioReportador.Id;
                }
            }

            CrearProyecto(proyecto);

            var p = _repositorioProyecto.Obtener(proyecto.Nombre);

            if (p.Bugs.Count > 0)
            {
                if(usuarioResuelve.Rol == Rol.Desarrollador) 
                {
                    p.Desarrolladores.Add(_repositorioUsuario.ObtenerDesarrollador(correoUsuarioResuelve, true));
                } 
                else
                {
                    p.Testers.Add(_repositorioUsuario.ObtenerTester(correoUsuarioResuelve, true));
                }

                foreach(var bug in p.Bugs)
                {
                    if(bug.Estado == Estado.Resuelto)
                    {
                        bug.ResueltoPorId = usuarioResuelve.Id;
                    }
                }

                _repositorioProyecto.Modificar(p);
            }

            
        }


        public ProyectoDTO ObtenerProyecto(int id)
        {
            return _mapper.Map<Proyecto, ProyectoDTO>(_repositorioProyecto.Obtener(id), opts => opts.AfterMap((src, dest) => {

                dest.CantBugs = dest.Bugs.Count;

                dest.Duracion = 0;

                dest.Costo = 0;

                // Calculo duracion del proyecto
                dest.Tareas.ForEach(tarea =>
                {
                    dest.Duracion += tarea.DuracionHoras;
                    dest.Costo += tarea.DuracionHoras * tarea.CostoPorHora;
                });

                dest.Bugs.ForEach(bug =>
                {
                    dest.Duracion += bug.DuracionHoras;

                    if (bug.Estado == Estado.Resuelto.ToString())
                    {
                        dest.Costo += bug.DuracionHoras * bug.ResueltoPor.CostoPorHora;
                    }
                });
            }));
        }

        public List<ProyectoDTO> ObtenerProyectos(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO.Rol == Rol.Administrador.ToString())
            {
                return _mapper.Map<List<Proyecto>, List<ProyectoDTO>>(_repositorioProyecto.Obtener(),
                    opt => opt.AfterMap((src, dest) => dest.ForEach(destObj =>
                    {
                        destObj.CantBugs = destObj.Bugs.Count;

                        destObj.Duracion = 0;

                        destObj.Costo = 0;

                        // Calculo duracion del proyecto
                        destObj.Tareas.ForEach(tarea =>
                        {
                            destObj.Duracion += tarea.DuracionHoras;
                            destObj.Costo += tarea.DuracionHoras * tarea.CostoPorHora;
                        });

                        destObj.Bugs.ForEach(bug => {
                            destObj.Duracion += bug.DuracionHoras;

                            if(bug.Estado == Estado.Resuelto.ToString())
                            {
                                destObj.Costo += bug.DuracionHoras * bug.ResueltoPor.CostoPorHora;
                            }
                        });
                    })));
            } else
            {

                var idProyectos = usuarioDTO.Proyectos.Select(proyecto => proyecto.Id);

                var proyectos = _repositorioProyecto.Obtener();

                return _mapper.Map<List<ProyectoDTO>>(proyectos.Where(proyecto => idProyectos.Contains(proyecto.Id)));

            }
            
        }

        public ProyectoDTO ObtenerProyecto(string nombre)
        {
            return _mapper.Map<Proyecto, ProyectoDTO>(_repositorioProyecto.Obtener(nombre), opts => opts.AfterMap((src, dest) => {

                dest.CantBugs = dest.Bugs.Count;

                dest.Duracion = 0;

                dest.Costo = 0;

                // Calculo duracion del proyecto
                dest.Tareas.ForEach(tarea =>
                {
                    dest.Duracion += tarea.DuracionHoras;
                    dest.Costo += tarea.DuracionHoras * tarea.CostoPorHora;
                });

                dest.Bugs.ForEach(bug =>
                {
                    dest.Duracion += bug.DuracionHoras;

                    if (bug.Estado == Estado.Resuelto.ToString())
                    {
                        dest.Costo += bug.DuracionHoras * bug.ResueltoPor.CostoPorHora;
                    }
                });
            }));
        }

        public void EliminarProyecto(int id)
        {
            var proyecto = _repositorioProyecto.Obtener(id);

            if (proyecto == null)
            {
                throw new ExcepcionProyectoInexistente(id);
            }

            _repositorioProyecto.Eliminar(id);
        }
        public void ModificarProyecto(int id, ProyectoDTO proyectoDTO)
        {
            var p = _repositorioProyecto.Obtener(id);

            if (p == null)
            {
                throw new ExcepcionProyectoInexistente(id);
            }
            else
            {
                var proyecto = _mapper.Map<Proyecto>(proyectoDTO);

                if (p.Nombre != proyecto.Nombre)
                {
                    var proyectoExistente = _repositorioProyecto.Obtener(proyecto.Nombre);

                    if (proyectoExistente != null)
                    {
                        throw new ExcepcionProyectoYaExistente(proyecto.Nombre);
                    }

                    p.Nombre = proyecto.Nombre;
                }

                if (proyecto.Bugs != null && proyecto.Bugs.Count > 0)
                {
                    p.Bugs = proyecto.Bugs;
                }

                _repositorioProyecto.Modificar(p);
            }
        }

        private Dictionary<string, object> ValidarAsignacion(int idProy, string correoElectronico, string rol, bool isAsignacion)
        {
            var p = _repositorioProyecto.Obtener(idProy);

            if (p == null)
            {
                throw new ExcepcionProyectoInexistente(idProy);
            }
            else
            {
                var exists = false;

                if (rol == Rol.Desarrollador.ToString())
                {
                    exists = p.Desarrolladores.ToList().Exists(desarrollador => desarrollador.CorreoElectronico == correoElectronico);
                }
                else if (rol == Rol.Tester.ToString())
                {
                    exists = p.Testers.ToList().Exists(tester => tester.CorreoElectronico == correoElectronico);
                }

                if (exists && isAsignacion)
                {
                    throw new ExcepcionUsuarioYaAsignado(correoElectronico);
                }

                if (!exists && !isAsignacion)
                {
                    throw new ExcepcionUsuarioNoAsignado(correoElectronico);
                }

                var usuario = _repositorioUsuario.Obtener(correoElectronico, false);

                if (usuario == null)
                {
                    throw new ExcepcionUsuarioInexistente(correoElectronico);
                }

                if (usuario.Rol.ToString() != rol)
                {
                    throw new ExcepcionDatosIncorrectos($"El rol del usuario {correoElectronico} no coincide");
                }

                var resultado = new Dictionary<string, object>();

                resultado.Add("usuario", usuario);

                resultado.Add("proyecto", p);

                return resultado;
            }
        }

        public void AsignarUsuario(int id, string correoElectronico, string rol)
        {
            var resultado = ValidarAsignacion(id, correoElectronico, rol, true);

            var proyecto = (Proyecto)resultado["proyecto"];

            var usuario = (Usuario)resultado["usuario"];

            _repositorioProyecto.AsignarUsuario(proyecto, usuario, rol);
        }

        public void DesasignarUsuario(int id, string correoElectronico, string rol)
        {
            var resultado = ValidarAsignacion(id, correoElectronico, rol, false);

            var proyecto = (Proyecto)resultado["proyecto"];

            var usuario = (Usuario)resultado["usuario"];

            _repositorioProyecto.DesasignarUsuario(proyecto, usuario, rol);
        }
    }
}