USE [master]
GO
/****** Object:  Database [JiruDB]    Script Date: 17/11/2021 23:02:46 ******/
CREATE DATABASE [JiruDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JiruDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JiruDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JiruDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\JiruDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JiruDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JiruDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JiruDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JiruDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JiruDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JiruDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JiruDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [JiruDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [JiruDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JiruDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JiruDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JiruDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JiruDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JiruDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JiruDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JiruDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JiruDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JiruDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JiruDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JiruDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JiruDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JiruDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JiruDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [JiruDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JiruDB] SET RECOVERY FULL 
GO
ALTER DATABASE [JiruDB] SET  MULTI_USER 
GO
ALTER DATABASE [JiruDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JiruDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JiruDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JiruDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JiruDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JiruDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [JiruDB] SET QUERY_STORE = OFF
GO
USE [JiruDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[ProyectoId] [int] NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[Estado] [int] NOT NULL,
	[ResueltoPorId] [int] NULL,
	[ReportadoPorId] [int] NOT NULL,
	[IdExterno] [nvarchar](max) NULL,
	[DuracionHoras] [float] NOT NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesarrolladorProyecto]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesarrolladorProyecto](
	[DesarrolladoresId] [int] NOT NULL,
	[ProyectosId] [int] NOT NULL,
 CONSTRAINT [PK_DesarrolladorProyecto] PRIMARY KEY CLUSTERED 
(
	[DesarrolladoresId] ASC,
	[ProyectosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyectos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](450) NULL,
 CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoTester]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoTester](
	[ProyectosId] [int] NOT NULL,
	[TestersId] [int] NOT NULL,
 CONSTRAINT [PK_ProyectoTester] PRIMARY KEY CLUSTERED 
(
	[ProyectosId] ASC,
	[TestersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[ProyectoId] [int] NOT NULL,
	[CostoPorHora] [float] NOT NULL,
	[DuracionHoras] [float] NOT NULL,
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 17/11/2021 23:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[NombreUsuario] [nvarchar](max) NULL,
	[Contrasena] [nvarchar](max) NULL,
	[CorreoElectronico] [nvarchar](450) NULL,
	[Rol] [int] NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[CostoPorHora] [float] NULL,
	[Desarrollador_CostoPorHora] [float] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211002153140_MigracionInicial', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211002210214_CambiosEnRelaciones', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211003002554_RelacionNoRequired', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211003015611_AgregadoIdExternoEnBug', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211003031604_CambiadoTipoDeDatoIdExterno', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211018235148_AgregadasTareas', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211020001107_CambiadoTipoDeDato', N'5.0.10')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Nombre], [Apellido], [NombreUsuario], [Contrasena], [CorreoElectronico], [Rol], [Discriminator], [CostoPorHora], [Desarrollador_CostoPorHora]) VALUES (1, N'ADMIN', N'ADMIN', N'ADMIN', N'$2a$11$Blm7sMprM5hB8.K1pr9Z9On/HqXLz.Ea8gqk17xCcw47V4chhQOEi', N'admin@jiru.com', 0, N'Administrador', NULL, NULL)
INSERT [dbo].[Usuarios] ([Id], [Nombre], [Apellido], [NombreUsuario], [Contrasena], [CorreoElectronico], [Rol], [Discriminator], [CostoPorHora], [Desarrollador_CostoPorHora]) VALUES (2, N'Erling', N'Haaland', N'erling@borussia.com', N'$2a$11$Lqfrsr39Ua8Hnjy3Dvc42OjBRslhl2qkFdJfJLNLsMbZMnd3UVfYC', N'erling@borussia.com', 2, N'Desarrollador', NULL, 125)
INSERT [dbo].[Usuarios] ([Id], [Nombre], [Apellido], [NombreUsuario], [Contrasena], [CorreoElectronico], [Rol], [Discriminator], [CostoPorHora], [Desarrollador_CostoPorHora]) VALUES (3, N'Robert', N'Lewandowski', N'tito', N'$2a$11$a5eoEQWXu5Z75LpldpGbLuHPfDEg86XVpmnLoq5ZgMR0dd7PuZqqC', N'robert.lewandoski@bayern.com', 1, N'Tester', 150, NULL)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Bugs_ProyectoId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_ProyectoId] ON [dbo].[Bugs]
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bugs_ReportadoPorId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_ReportadoPorId] ON [dbo].[Bugs]
(
	[ReportadoPorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bugs_ResueltoPorId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_ResueltoPorId] ON [dbo].[Bugs]
(
	[ResueltoPorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DesarrolladorProyecto_ProyectosId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_DesarrolladorProyecto_ProyectosId] ON [dbo].[DesarrolladorProyecto]
(
	[ProyectosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Proyectos_Nombre]    Script Date: 17/11/2021 23:02:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Proyectos_Nombre] ON [dbo].[Proyectos]
(
	[Nombre] ASC
)
WHERE ([Nombre] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProyectoTester_TestersId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_ProyectoTester_TestersId] ON [dbo].[ProyectoTester]
(
	[TestersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tareas_ProyectoId]    Script Date: 17/11/2021 23:02:46 ******/
CREATE NONCLUSTERED INDEX [IX_Tareas_ProyectoId] ON [dbo].[Tareas]
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Usuarios_CorreoElectronico]    Script Date: 17/11/2021 23:02:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_CorreoElectronico] ON [dbo].[Usuarios]
(
	[CorreoElectronico] ASC
)
WHERE ([CorreoElectronico] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bugs] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [DuracionHoras]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Proyectos_ProyectoId] FOREIGN KEY([ProyectoId])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Proyectos_ProyectoId]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Usuarios_ReportadoPorId] FOREIGN KEY([ReportadoPorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Usuarios_ReportadoPorId]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Usuarios_ResueltoPorId] FOREIGN KEY([ResueltoPorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Usuarios_ResueltoPorId]
GO
ALTER TABLE [dbo].[DesarrolladorProyecto]  WITH CHECK ADD  CONSTRAINT [FK_DesarrolladorProyecto_Proyectos_ProyectosId] FOREIGN KEY([ProyectosId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DesarrolladorProyecto] CHECK CONSTRAINT [FK_DesarrolladorProyecto_Proyectos_ProyectosId]
GO
ALTER TABLE [dbo].[DesarrolladorProyecto]  WITH CHECK ADD  CONSTRAINT [FK_DesarrolladorProyecto_Usuarios_DesarrolladoresId] FOREIGN KEY([DesarrolladoresId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DesarrolladorProyecto] CHECK CONSTRAINT [FK_DesarrolladorProyecto_Usuarios_DesarrolladoresId]
GO
ALTER TABLE [dbo].[ProyectoTester]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoTester_Proyectos_ProyectosId] FOREIGN KEY([ProyectosId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProyectoTester] CHECK CONSTRAINT [FK_ProyectoTester_Proyectos_ProyectosId]
GO
ALTER TABLE [dbo].[ProyectoTester]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoTester_Usuarios_TestersId] FOREIGN KEY([TestersId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProyectoTester] CHECK CONSTRAINT [FK_ProyectoTester_Usuarios_TestersId]
GO
ALTER TABLE [dbo].[Tareas]  WITH CHECK ADD  CONSTRAINT [FK_Tareas_Proyectos_ProyectoId] FOREIGN KEY([ProyectoId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tareas] CHECK CONSTRAINT [FK_Tareas_Proyectos_ProyectoId]
GO
USE [master]
GO
ALTER DATABASE [JiruDB] SET  READ_WRITE 
GO
