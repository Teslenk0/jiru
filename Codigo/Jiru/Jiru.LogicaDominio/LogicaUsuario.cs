using BCryptNet = BCrypt.Net.BCrypt;
using AutoMapper;
using Jiru.Dominio;
using Jiru.DTOs;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using Jiru.Excepciones.Base;
using System.Collections.Generic;

namespace Jiru.LogicaDominio
{
    public class LogicaUsuario : ILogicaUsuario
    {

        private readonly IRepositorioUsuario _repositorioUsuario;

        private readonly IMapper _mapper;

        public LogicaUsuario(IRepositorioUsuario usuarioRepositorio, IMapper mapper)
        {
            this._repositorioUsuario = usuarioRepositorio;

            this._mapper = mapper;
        }

        public void CrearAdministrador(UsuarioDTO usuario)
        {
            Administrador u = _mapper.Map<Administrador>(usuario);
           
            u.Rol = Rol.Administrador;

            CrearUsuario(u);
        }

        public void CrearDesarrollador(UsuarioDTO usuario)
        {
            Desarrollador u = _mapper.Map<Desarrollador>(usuario);

            u.Rol = Rol.Desarrollador;

            CrearUsuario(u);
        }

        public void CrearTester(UsuarioDTO usuario)
        {
            Tester u = _mapper.Map<Tester>(usuario);

            u.Rol = Rol.Tester;

            CrearUsuario(u);
        }

        private void CrearUsuario(Usuario usuario)
        {
            var usuarioExistente = _repositorioUsuario.Obtener(usuario.CorreoElectronico);

            if(usuarioExistente != null)
            {
                throw new ExcepcionUsuarioYaExistente(usuario.CorreoElectronico);
            }

            usuario.Contrasena = BCryptNet.HashPassword(usuario.Contrasena);

            _repositorioUsuario.Crear(usuario);
        }

        public UsuarioDTO ObtenerUsuario(string correoElectronico)
        {
            return _mapper.Map<UsuarioDTO>(_repositorioUsuario.Obtener(correoElectronico));
        }

        public UsuarioDTO ObtenerUsuario(int id)
        {
            return _mapper.Map<UsuarioDTO>(_repositorioUsuario.Obtener(id));
        }
    }
}