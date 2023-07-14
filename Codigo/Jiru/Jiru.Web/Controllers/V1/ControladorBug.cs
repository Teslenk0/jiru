using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/bug")]
    [ApiController]
    public class ControladorBug : ControllerBase
    {
        private readonly ILogicaBug _logicaBug;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public ControladorBug(ILogicaBug logicaBug, IHttpContextAccessor httpContextAccessor)
        {
            _logicaBug = logicaBug;

            _httpContextAccessor = httpContextAccessor;
        }

        
        [HttpGet]
        [FiltroAutenticacion("Administrador", "Tester", "Desarrollador")]
        public ActionResult Obtener([FromQuery] FiltroDTO filtroDTO)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaBug.ObtenerBugs(filtroDTO, usuario),
            };

            return Ok(respuesta);
        }

        [HttpGet("desarrollador/{correoElectronico}/resuelto")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult ObtenerCantidadDeBugsPorDesarrollador(string correoElectronico)
        {
            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaBug.ObtenerCantidadDeBugsPorUsuario(correoElectronico)
            };

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [FiltroAutenticacion("Administrador", "Tester", "Desarrollador")]
        public ActionResult Obtener(int id)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaBug.ObtenerBug(id, usuario)
            };

            return Ok(respuesta);
        }

        [HttpPost]
        [FiltroAutenticacion("Administrador", "Tester")]
        public ActionResult Crear([FromBody] BugDTO bugDTO)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            _logicaBug.CrearBug(bugDTO, usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Bug creado exitosamente."
            };

            return Created("", respuesta);
        }

        [HttpPut("{id}")]
        [FiltroAutenticacion("Administrador", "Tester", "Desarrollador")]
        public ActionResult Modificar(int id, [FromBody] BugDTO bugDTO)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            _logicaBug.ModificarBug(id, bugDTO, usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Bug modificado exitosamente."
            };

            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [FiltroAutenticacion("Administrador", "Tester")]
        public ActionResult Eliminar(int id)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            _logicaBug.EliminarBug(id, usuario);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Bug eliminado exitosamente."
            };

            return Ok(respuesta);
        }
    }
}
