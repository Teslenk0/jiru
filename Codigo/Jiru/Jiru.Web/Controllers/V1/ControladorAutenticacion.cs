using Jiru.DTOs;
using Jiru.ILogicaDominio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/autenticacion")]
    [ApiController]
    public class ControladorAutenticacion : ControllerBase
    {
        private readonly ILogicaAutenticacion _logicaAutenticacion;


        public ControladorAutenticacion(ILogicaAutenticacion logicaAutenticacion)
        {
            _logicaAutenticacion = logicaAutenticacion;
        }

        [HttpPost("acceder")]
        public ActionResult Acceder([FromBody] AutenticacionDTO autenticacionDTO)
        {
            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Resultado = _logicaAutenticacion.IniciarSesion(autenticacionDTO)
            };

            return Ok(respuesta);
        }
    }
}
