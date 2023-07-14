using Jiru.Configuracion;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IImportacion;
using Jiru.ILogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Jiru.Web.Controllers.V1
{
    [Route("api/v1/importacion")]
    [ApiController]
    public class ControladorImportacion : ControllerBase
    {
        ILogicaProyecto _logicaProyecto;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ControladorImportacion(IHttpContextAccessor httpContextAccessor, ILogicaProyecto logicaProyecto)
        {
            _logicaProyecto = logicaProyecto;

            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [FiltroAutenticacion("Administrador")]
        public ActionResult Importar([FromBody] ImportacionDTO importacionDTO)
        {
            UsuarioDTO usuario = (UsuarioDTO)_httpContextAccessor.HttpContext.Items["usuario"];

            List<PluginInfoDTO> plugins = ObtenerLibreriasImportacionDisponibles();

            PluginInfoDTO seleccionado = plugins.Where(p => p.Nombre.ToUpper().Contains(importacionDTO.Proveedor.ToUpper())).FirstOrDefault();

            if (seleccionado == null)
            {
                throw new ExcepcionProveedorNoSoportado();
            }

            ILogicaImportacion logicaImportacion = (ILogicaImportacion)ActivatorUtilities.CreateInstance(null, seleccionado.TipoImportacion);

            ProyectoDTO proyectoDTO = logicaImportacion.ImportarBugs(importacionDTO.Parametros);

            _logicaProyecto.ImportarProyecto(proyectoDTO, usuario, importacionDTO.Parametros["correoUsuarioResuelve"]);

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Bugs importados exitosamente"
            };

            return Ok(respuesta);
        }

        [HttpGet("disponible")]
        [FiltroAutenticacion("Administrador")]
        public ActionResult ObtenerImportadores()
        {
            List<PluginInfoDTO> plugins = ObtenerLibreriasImportacionDisponibles();

            RespuestaDTO respuesta = new RespuestaDTO()
            {
                Exito = true,
                CodigoEstado = (int)HttpStatusCode.OK,
                Mensaje = "Listado de importadores disponibles",
                Resultado = plugins
            };

            return Ok(respuesta);
        }

        private List<PluginInfoDTO> ObtenerLibreriasImportacionDisponibles()
        {
            Type logicaImportacion = typeof(ILogicaImportacion);

            List<PluginInfoDTO> plugins = new List<PluginInfoDTO>();

            try
            {
                Console.WriteLine("Carpeta DLLs: " + ManejadorConfiguracion.CarpetaContenedoraLibs);

                foreach (string nombreDll in System.IO.Directory.GetFiles(ManejadorConfiguracion.CarpetaContenedoraLibs, "*.dll"))
                {
                    //1) Creo una instancia de Assembly, dado su nombre
                    Assembly assemblyDinamico = Assembly.LoadFrom(nombreDll);

                    //2) Buco una clase definida en ese assembly, que implemente la interfaz
                    var tipo = assemblyDinamico.GetTypes(). //De todos los tipos que esten definidos en el assembly
                        Where(t => logicaImportacion.IsAssignableFrom(t) //Me quedo con los que que se podrian asignar a la intefaz ILogicaImportacion
                              && t != logicaImportacion) //y debo excluir al que me define la interfaz en si misma.
                        .FirstOrDefault(); //Como asumo de antemano que tengo una sola clase por dll que implementa esa interfaz, lo selecciono

                    //3) Si encontre algo que implemente ILogicaImportacion (un plugin), lo agrego a la lista de opciones.
                    if (tipo != null)
                    {
                        string[] arrNombre = nombreDll.Split('.');

                        plugins.Add(new PluginInfoDTO
                        {
                            Nombre = $"{arrNombre[2]} - {arrNombre[3]}",
                            RutaAlArchivo = nombreDll,
                            TipoImportacion = tipo,
                            Proveedor = arrNombre[3]
                        });
                    }
                }
                return plugins;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return plugins;
            }
        }
    }
}
