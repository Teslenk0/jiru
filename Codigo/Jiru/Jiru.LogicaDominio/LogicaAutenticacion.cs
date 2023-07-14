using BCryptNet = BCrypt.Net.BCrypt;
using AutoMapper;
using Jiru.DTOs;
using Jiru.Excepciones.Base;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using Jiru.Dominio;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Jiru.Configuracion;
using System.Linq;

namespace Jiru.LogicaDominio
{
    public class LogicaAutenticacion : ILogicaAutenticacion
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        private readonly IMapper _mapper;

        public LogicaAutenticacion(IRepositorioUsuario usuarioRepositorio, IMapper mapper)
        {
            this._repositorioUsuario = usuarioRepositorio;

            this._mapper = mapper;
        }

        public RespuestaAutenticacionDTO IniciarSesion(AutenticacionDTO credenciales)
        {
            var usuario = _repositorioUsuario.Obtener(credenciales.CorreoElectronico);

            if (usuario == null)
            {
                throw new ExcepcionUsuarioInexistente(credenciales.CorreoElectronico);
            }

            if (!BCryptNet.Verify(credenciales.Contrasena, usuario.Contrasena))
            {
                throw new ExcepcionContrasenaIncorrecta();
            }

            var resultado = new RespuestaAutenticacionDTO()
            {
                Usuario = _mapper.Map<UsuarioDTO>(usuario),
                Token = GenerarJwt(usuario)
            };

            return resultado;
        }

        public UsuarioDTO ValidarToken(string token)
        {
            try
            {
                if(token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }

                var manejadorTokens = new JwtSecurityTokenHandler();

                var clave = Encoding.ASCII.GetBytes(ManejadorConfiguracion.ClaveJwt);

                manejadorTokens.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(clave),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                }, out SecurityToken tokenValidado);

                var jwtToken = (JwtSecurityToken)tokenValidado;

                var correoElectronico = jwtToken.Claims.First(x => x.Type == "CorreoElectronico").Value;

                return _mapper.Map<UsuarioDTO>(_repositorioUsuario.Obtener(correoElectronico));
                
            }
            catch
            {
                return null;
            }
        }

        private string GenerarJwt(Usuario usuario)
        {

            var manejadorTokens = new JwtSecurityTokenHandler();

            var clave = Encoding.ASCII.GetBytes(ManejadorConfiguracion.ClaveJwt);

            var datosToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("CorreoElectronico", usuario.CorreoElectronico.ToString()) }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorTokens.CreateToken(datosToken);

            return manejadorTokens.WriteToken(token);
        }
    }
}
