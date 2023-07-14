﻿// <auto-generated />
using System;
using Jiru.AccesoADatos.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jiru.Web.Migrations
{
    [DbContext(typeof(JiruDbContext))]
    partial class JiruDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DesarrolladorProyecto", b =>
                {
                    b.Property<int>("DesarrolladoresId")
                        .HasColumnType("int");

                    b.Property<int>("ProyectosId")
                        .HasColumnType("int");

                    b.HasKey("DesarrolladoresId", "ProyectosId");

                    b.HasIndex("ProyectosId");

                    b.ToTable("DesarrolladorProyecto");
                });

            modelBuilder.Entity("Jiru.Dominio.Bug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DuracionHoras")
                        .HasColumnType("float");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("IdExterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("int");

                    b.Property<int>("ReportadoPorId")
                        .HasColumnType("int");

                    b.Property<int?>("ResueltoPorId")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("ReportadoPorId");

                    b.HasIndex("ResueltoPorId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("Jiru.Dominio.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique()
                        .HasFilter("[Nombre] IS NOT NULL");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("Jiru.Dominio.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CostoPorHora")
                        .HasColumnType("float");

                    b.Property<double>("DuracionHoras")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("Jiru.Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CorreoElectronico")
                        .IsUnique()
                        .HasFilter("[CorreoElectronico] IS NOT NULL");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("ProyectoTester", b =>
                {
                    b.Property<int>("ProyectosId")
                        .HasColumnType("int");

                    b.Property<int>("TestersId")
                        .HasColumnType("int");

                    b.HasKey("ProyectosId", "TestersId");

                    b.HasIndex("TestersId");

                    b.ToTable("ProyectoTester");
                });

            modelBuilder.Entity("Jiru.Dominio.Administrador", b =>
                {
                    b.HasBaseType("Jiru.Dominio.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("Jiru.Dominio.Desarrollador", b =>
                {
                    b.HasBaseType("Jiru.Dominio.Usuario");

                    b.Property<double>("CostoPorHora")
                        .HasColumnType("float")
                        .HasColumnName("Desarrollador_CostoPorHora");

                    b.HasDiscriminator().HasValue("Desarrollador");
                });

            modelBuilder.Entity("Jiru.Dominio.Tester", b =>
                {
                    b.HasBaseType("Jiru.Dominio.Usuario");

                    b.Property<double>("CostoPorHora")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Tester");
                });

            modelBuilder.Entity("DesarrolladorProyecto", b =>
                {
                    b.HasOne("Jiru.Dominio.Desarrollador", null)
                        .WithMany()
                        .HasForeignKey("DesarrolladoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jiru.Dominio.Proyecto", null)
                        .WithMany()
                        .HasForeignKey("ProyectosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jiru.Dominio.Bug", b =>
                {
                    b.HasOne("Jiru.Dominio.Proyecto", "Proyecto")
                        .WithMany("Bugs")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Jiru.Dominio.Usuario", "ReportadoPor")
                        .WithMany()
                        .HasForeignKey("ReportadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Jiru.Dominio.Usuario", "ResueltoPor")
                        .WithMany()
                        .HasForeignKey("ResueltoPorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Proyecto");

                    b.Navigation("ReportadoPor");

                    b.Navigation("ResueltoPor");
                });

            modelBuilder.Entity("Jiru.Dominio.Tarea", b =>
                {
                    b.HasOne("Jiru.Dominio.Proyecto", "Proyecto")
                        .WithMany("Tareas")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyecto");
                });

            modelBuilder.Entity("ProyectoTester", b =>
                {
                    b.HasOne("Jiru.Dominio.Proyecto", null)
                        .WithMany()
                        .HasForeignKey("ProyectosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jiru.Dominio.Tester", null)
                        .WithMany()
                        .HasForeignKey("TestersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jiru.Dominio.Proyecto", b =>
                {
                    b.Navigation("Bugs");

                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}