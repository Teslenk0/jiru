#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV PATH $PATH:/root/.dotnet/tools
RUN apt update && apt install dos2unix
WORKDIR /src
COPY ["Jiru.Web/Jiru.Web.csproj", "Jiru.Web/"]
COPY ["Jiru.LogicaDominio/Jiru.LogicaDominio.csproj", "Jiru.LogicaDominio/"]
COPY ["Jiru.IAccesoADatos/Jiru.IAccesoADatos.csproj", "Jiru.IAccesoADatos/"]
COPY ["Jiru.Dominio/Jiru.Dominio.csproj", "Jiru.Dominio/"]
COPY ["Jiru.DTOs/Jiru.DTOs.csproj", "Jiru.DTOs/"]
COPY ["Jiru.Utilidades/Jiru.Utilidades.csproj", "Jiru.Utilidades/"]
COPY ["Jiru.Configuracion/Jiru.Configuracion.csproj", "Jiru.Configuracion/"]
COPY ["Jiru.Excepciones/Jiru.Excepciones.csproj", "Jiru.Excepciones/"]
COPY ["Jiru.ILogicaDominio/Jiru.ILogicaDominio.csproj", "Jiru.ILogicaDominio/"]
COPY ["Jiru.ILogicaImportacion/Jiru.IImportacion.csproj", "Jiru.ILogicaImportacion/"]
COPY ["Jiru.AccesoADatos/Jiru.AccesoADatos.csproj", "Jiru.AccesoADatos/"]
RUN dotnet restore "Jiru.Web/Jiru.Web.csproj"
COPY . .
WORKDIR "/src/Jiru.Web"
RUN dos2unix ./wait-for-it.sh
RUN dotnet build "Jiru.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jiru.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /opt/jirulibs
RUN mkdir -p /opt/importaciones
RUN cp -R /app/Importaciones/* /opt/importaciones

RUN chmod +x /app/wait-for-it.sh
#ENTRYPOINT ["dotnet", "Jiru.Web.dll"]
ENTRYPOINT ["/app/wait-for-it.sh", "jiru_db:1433", "-t", "120", "--", "dotnet", "Jiru.Web.dll"]