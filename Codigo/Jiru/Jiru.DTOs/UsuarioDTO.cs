using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Jiru.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre de usuario.")]
        public string NombreUsuario { get; set; }

        [EmailAddress(ErrorMessage = "Por favor ingrese un correo electronico valido.")]
        [Required(ErrorMessage = "Por favor ingrese un correo electronico.")]
        public string CorreoElectronico { get; set; }

        public string Rol { get; set; }

        public List<ProyectoDTO> Proyectos { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una contraseña.")]
        public string Contrasena { get; set; }

        public double CostoPorHora { get; set; }
    }
}
