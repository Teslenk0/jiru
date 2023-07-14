using AutoMapper;
using Jiru.DTOs;
using Jiru.Dominio;

namespace Jiru.Configuracion
{
    public class PerfilAutoMapper : Profile
    {
        public PerfilAutoMapper()
        {

            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Proyecto, ProyectoDTO>().ReverseMap();

            CreateMap<Bug, BugDTO>().ReverseMap();

            CreateMap<Tarea, TareaDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioDTO>().ForMember(el => el.Contrasena, contrasena => contrasena.Ignore());

            CreateMap<Desarrollador, UsuarioDTO>().ForMember(el => el.Contrasena, contrasena => contrasena.Ignore());
            CreateMap<UsuarioDTO, Desarrollador>();

            CreateMap<Usuario, Desarrollador>();

            CreateMap<Tester, UsuarioDTO>().ForMember(el => el.Contrasena, contrasena => contrasena.Ignore());
            CreateMap<UsuarioDTO, Tester>();

            CreateMap<Usuario, Tester>();

            CreateMap<Administrador, UsuarioDTO>().ForMember(el => el.Contrasena, contrasena => contrasena.Ignore());

            CreateMap<UsuarioDTO, Administrador>();

            CreateMap<Usuario, Administrador>();
        }

    }
}
