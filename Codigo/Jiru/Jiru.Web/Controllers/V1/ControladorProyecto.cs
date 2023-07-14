using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/proyecto")]
    [ApiController]
    public class ControladorProyecto : ControllerBase
    {
        private readonly ILogicaProyecto _logicaProyecto;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public ControladorProyecto(ILogicaProyecto logicaProyecto, IHttpContextAccessor httpContextAccessor)
        {
            _logicaProyecto = logicaProyecto;

            _httpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet]
        [FiltroAutenticacion("Administrador", "Tester", "Desarrollador")]
        public ActionResult Obtener()
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaProyecto.ObtenerProyectos(usuario),
            };

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Obtener(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaProyecto.ObtenerProyecto(id),
            };

            return Ok(respuesta);
        }

        [HttpPost]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Crear([FromBody] ProyectoDTO proyecto)
        {

            _logicaProyecto.CrearProyecto(proyecto);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Proyecto creado exitosamente."
            };

            return Created("", respuesta);
        }

        [HttpPut("{id}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Modificar(int id, [FromBody] ProyectoDTO proyecto)
        {
            _logicaProyecto.ModificarProyecto(id, proyecto);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Proyecto modificado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpPut("{id}/desarrollador/{correoElectronico}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult AsignarDesarrollador(int id, string correoElectronico)
        {
            _logicaProyecto.AsignarUsuario(id, correoElectronico, "Desarrollador");

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Usuario asignado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpDelete("{id}/desarrollador/{correoElectronico}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult DesasignarDesarrollador(int id, string correoElectronico)
        {
            _logicaProyecto.DesasignarUsuario(id, correoElectronico, "Desarrollador");

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Usuario desasignado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpPut("{id}/tester/{correoElectronico}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult AsignarTester(int id, string correoElectronico)
        {
            _logicaProyecto.AsignarUsuario(id, correoElectronico, "Tester");

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Usuario asignado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpDelete("{id}/tester/{correoElectronico}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult DesasignarTester(int id, string correoElectronico)
        {
            _logicaProyecto.DesasignarUsuario(id, correoElectronico, "Tester");

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Usuario desasignado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Eliminar(int id)
        {
            _logicaProyecto.EliminarProyecto(id);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Proyecto eliminado exitosamente."
            };

            return Ok(respuesta);
        }
    }
}
