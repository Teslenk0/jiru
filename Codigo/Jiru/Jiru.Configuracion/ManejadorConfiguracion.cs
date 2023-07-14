
using Microsoft.Extensions.Configuration;
using System;

namespace Jiru.Configuracion
{
    public static class ManejadorConfiguracion
    {
        private static IConfigurationSection _configuracion;

        private static IConfigurationSection Instancia
        {
            get
            {
                if (_configuracion == null)
                {

                    var environment = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") : "Production";
                    
                    Console.WriteLine(environment);

                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{environment}.json", true, true).Build();
                    _configuracion = configuration.GetSection("ConfiguracionApp");
                }

                return _configuracion;
            }
        }

        public static string CarpetaContenedoraLibs
        {
            get
            {
                return Instancia["CarpetaContenedoraLibs"];
            }
        }

        // Estos datos no deberian estar pusheado en el repositorio de GitHub.
        // Simplemente se intenta evitar agregar complejidad no necesaria.
        // Otro lugar para almacenarlo podria ser AppConfig o ParameterStore de AWS
        public static string ClaveJwt
        {
            get
            {
                return Instancia["ClaveJwt"];
            }
        }

        public static string ObtenerNombreArchivoProveedor(string proveedor)
        {
            return Instancia.GetSection(proveedor)["ArchivoImportacion"];
        }

    }
}
