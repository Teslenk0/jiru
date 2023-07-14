using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class ControladorUsuario : ControllerBase
    {
        private readonly ILogicaUsuario _logicaUsuario;

        public ControladorUsuario(ILogicaUsuario logicaUsuario)
        {
            _logicaUsuario = logicaUsuario;
        }


        [HttpPost("desarrollador")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult CrearDesarrollador([FromBody] UsuarioDTO usuario)
        {

            _logicaUsuario.CrearDesarrollador(usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Usuario creado exitosamente."
            };

            return Created("", respuesta);
        }

        [HttpPost("administrador")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult CrearAdministrador([FromBody] UsuarioDTO usuario)
        {
            _logicaUsuario.CrearAdministrador(usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Usuario creado exitosamente."
            };

            return Created("", respuesta);
        }

        [HttpPost("tester")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult CrearTester([FromBody] UsuarioDTO usuario)
        {
            _logicaUsuario.CrearTester(usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Usuario creado exitosamente."
            };

            return Created("", respuesta);
        }
    }
}
