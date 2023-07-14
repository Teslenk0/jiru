using Jiru.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Jiru.AccesoADatos.Config
{
    public class JiruDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Administrador> Administradores { get; set; }

        public DbSet<Tester> Testers { get; set; }

        public DbSet<Desarrollador> Desarrolladores { get; set; }

        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Bug> Bugs { get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        public JiruDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>()
                .HasIndex(s => s.CorreoElectronico)
                .IsUnique();

            builder.Entity<Proyecto>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

            builder
                .Entity<Proyecto>()
                .HasMany(p => p.Bugs)
                .WithOne(b => b.Proyecto)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .Entity<Bug>()
                .HasOne(b => b.ReportadoPor)
                .WithMany()
                .HasForeignKey(b => b.ReportadoPorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Bug>()
                .HasOne(b => b.ResueltoPor)
                .WithMany()
                .HasForeignKey(b => b.ResueltoPorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }

}
