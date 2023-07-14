using Jiru.Excepciones.Base;
using Jiru.ILogicaDominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Text.Json;

namespace Jiru.Web.Filtros
{
    public class FiltroAutenticacion : Attribute, IAuthorizationFilter
    {
        private ILogicaAutenticacion _logicaAutenticacion;

        private readonly string[] _roles;

        public FiltroAutenticacion(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            _logicaAutenticacion = (ILogicaAutenticacion)contexto.HttpContext.RequestServices.GetService(typeof(ILogicaAutenticacion));

            string token = contexto.HttpContext.Request.Headers["Authorization"];

            if (token == null)
            {
                var result = JsonSerializer.Serialize(new { Mensaje = "Acceso denegado. Por favor valida tus credenciales.", CodigoEstado = 401, Exito = false });

                contexto.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = result,
                    ContentType = "application/json"
                };

                return;
            }

            var usuario = _logicaAutenticacion.ValidarToken(token);

            if (usuario == null)
            {
                var result = JsonSerializer.Serialize(new { Mensaje = "Acceso denegado. Por favor valida tus credenciales.", CodigoEstado = 401, Exito = false });

                contexto.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = result,
                    ContentType = "application/json"
                };

                return;
            }

            if (!_roles.Contains(usuario.Rol))
            {
                var result = JsonSerializer.Serialize(new { Mensaje = "Acceso denegado. No tienes permiso para realizar la acción solicitada.", CodigoEstado = 403, Exito = false });

                contexto.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = result,
                    ContentType = "application/json"
                };

                return;
            }

            contexto.HttpContext.Items.Add("usuario", usuario);
        }
    }
}
