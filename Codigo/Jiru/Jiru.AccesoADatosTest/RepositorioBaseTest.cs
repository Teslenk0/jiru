using Jiru.AccesoADatos.Config;
using Jiru.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Jiru.AccesoADatosTest
{
    public class RepositorioBaseTest
    {
        protected JiruDbContext DBContext
        {
            get;
            private set;
        }

        public void LevantarBase()
        {
            var options = new DbContextOptionsBuilder<JiruDbContext>().UseInMemoryDatabase(databaseName: "JiruDB").Options;

            DBContext = new JiruDbContext(options);

            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Brahian",
                Apellido = "Pena",
                Contrasena = "uipqbrfe9ubqu9wehbr",
                NombreUsuario = "Reportador",
                CorreoElectronico = "teslasapbe@gmail.com",
                Rol = Rol.Administrador
            };

            Desarrollador desarrollador = new Desarrollador()
            {
                Id = 2,
                Nombre = "Ricardo",
                Apellido = "Fort",
                NombreUsuario = "Comandante",
                Contrasena = "ElRichestRicky78@pEpe?",
                CorreoElectronico = "dev.ricky@gmailcom",
                Rol = Rol.Desarrollador
            };

            Tester tester = new Tester()
            {
                Id = 3,
                Nombre = "Ricky",
                Apellido = "Musso",
                NombreUsuario = "Guitarra",
                Contrasena = "ricky@pEpe?",
                CorreoElectronico = "el.ricky@gmailcom",
                Rol = Rol.Tester
            };

            Bug b1 = new Bug()
            {
                Nombre = "Problema que no carga",
                Version = "1.0.0",
                Descripcion = "No carga nada",
                Estado = Estado.Activo
            };

            Bug b2 = new Bug()
            {
                Nombre = "Problema con el header",
                Version = "2.0.0",
                Descripcion = "Color incorrecto",
                Estado = Estado.Resuelto,
                ResueltoPor = usuario
            };

            Bug b3 = new Bug()
            {
                Nombre = "Video incorrecto",
                Version = "1.4.0",
                Descripcion = "Apuntando al servicio que no es",
                Estado = Estado.Activo
            };

            Proyecto p1 = new Proyecto()
            {
                Nombre = "YouTube",
                Bugs = new List<Bug>(),
                Desarrolladores = new List<Desarrollador>(),
                Testers = new List<Tester>()
            };

            Proyecto p2 = new Proyecto()
            {
                Nombre = "Jiru",
                Bugs = new List<Bug>(),
                Desarrolladores = new List<Desarrollador>(),
                Testers = new List<Tester>()
            };

            p1.Bugs.Add(b1);
            p1.Bugs.Add(b2);
            p2.Bugs.Add(b3);

            DBContext.Desarrolladores.Add(desarrollador);
            DBContext.Testers.Add(tester);

            DBContext.Add(p1);
            DBContext.Add(p2);

            DBContext.SaveChanges();
        }

        protected void LimpiarBase()
        {
            DBContext.Database.EnsureDeleted();
        }
    }
}
