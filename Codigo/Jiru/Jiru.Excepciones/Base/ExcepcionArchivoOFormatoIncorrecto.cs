using System;

namespace Jiru.Excepciones.Base
{
    public class ExcepcionArchivoOFormatoIncorrecto : Exception
    {
        public ExcepcionArchivoOFormatoIncorrecto() : base("El archivo ingresado para la importacion no existe o el formato es incorrecto") { }
    }
}
