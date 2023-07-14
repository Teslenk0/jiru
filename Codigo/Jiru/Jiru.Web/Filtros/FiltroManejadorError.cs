using Jiru.Excepciones.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text.Json;

namespace Jiru.Web.Filtros
{
    public class FiltroManejadorError : Attribute, IExceptionFilter
    {
        private IWebHostEnvironment _ambiente { get; set; }

        public FiltroManejadorError(IWebHostEnvironment ambiente) : base()
        {
            _ambiente = ambiente;
        }
        public void OnException(ExceptionContext contexto)
        {
            HttpStatusCode codigoEstado;
            Exception excepcion = contexto.Exception;
            string mensaje = excepcion.Message;
            var stackTrace = String.Empty;
            var tipoExcepcion = excepcion.GetType();

            if (tipoExcepcion == typeof(ExcepcionBugInexistente) ||
                tipoExcepcion == typeof(ExcepcionProyectoInexistente) ||
                tipoExcepcion == typeof(ExcepcionUsuarioInexistente))
            {
                codigoEstado = HttpStatusCode.NotFound;
            }
            else if (tipoExcepcion == typeof(ExcepcionUsuarioYaExistente) ||
                    tipoExcepcion == typeof(ExcepcionProyectoYaExistente) ||
                    tipoExcepcion == typeof(ExcepcionUsuarioYaAsignado) ||
                    tipoExcepcion == typeof(ExcepcionUsuarioNoAsignado))
            {
                codigoEstado = HttpStatusCode.Conflict;
            }
            else if (tipoExcepcion == typeof(ExcepcionAccesoDenegado))
            {
                codigoEstado = HttpStatusCode.Forbidden;
            }
            else if (tipoExcepcion == typeof(ExcepcionContrasenaIncorrecta))
            {
                codigoEstado = HttpStatusCode.Unauthorized;
            }
            else if (tipoExcepcion == typeof(ExcepcionProveedorNoSoportado) ||
                    tipoExcepcion == typeof(ExcepcionDatosIncorrectos))
            {
                codigoEstado = HttpStatusCode.BadRequest;
            }
            else if (tipoExcepcion == typeof(ExcepcionArchivoOFormatoIncorrecto))
            {
                codigoEstado = HttpStatusCode.UnprocessableEntity;
            }
            else
            {
                codigoEstado = HttpStatusCode.InternalServerError;

                if (_ambiente.IsEnvironment("Development"))
                    stackTrace = excepcion.StackTrace;
            }

            var result = JsonSerializer.Serialize(new { Mensaje = mensaje, StackTrace = stackTrace, CodigoEstado = codigoEstado, Exito = false });

            contexto.Result = new ContentResult()
            {
                StatusCode = (int)codigoEstado,
                Content = result,
                ContentType = "application/json"
            };
        }
    }
}
