using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiru.DTOs
{
    public class AutenticacionDTO
    {
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo electronico valido.")]
        [Required(ErrorMessage = "Por favor ingrese un correo electronico.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una contraseña.")]
        public string Contrasena { get; set; }
    }
}
