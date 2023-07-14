using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/tarea")]
    [ApiController]
    public class ControladorTarea : ControllerBase
    {
        private readonly ILogicaTarea _logicaTarea;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ControladorTarea(ILogicaTarea logicaTarea, IHttpContextAccessor httpContextAccessor)
        {
            _logicaTarea = logicaTarea;

            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Crear([FromBody] TareaDTO tareaDTO)
        {
            _logicaTarea.CrearTarea(tareaDTO);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.Created,
                Mensaje = "Tarea creada exitosamente."
            };

            return Created("", respuesta);
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
                Resultado = _logicaTarea.ObtenerTareas(usuario),
            };

            return Ok(respuesta);
        }

    }
}
