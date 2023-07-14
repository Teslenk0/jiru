using Jiru.AccesoADatos.Config;
using Jiru.AccesoADatos.Repositorios;
using Jiru.Configuracion;
using Jiru.IAccesoADatos;
using Jiru.ILogicaDominio;
using Jiru.LogicaDominio;
using Jiru.Web.Filtros;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Jiru.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PerfilAutoMapper));

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            ));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMvc(options =>
            {
                options.Filters.Add(new FiltroManejadorError(Environment));
            });

            services.AddDbContext<JiruDbContext>(opts =>
             opts.UseSqlServer(Configuration.GetConnectionString("conexionJiruSql"),
             b => b.MigrationsAssembly("Jiru.Web")));

            services.AddHttpContextAccessor();

            services.AddScoped<ILogicaAutenticacion, LogicaAutenticacion>();
            services.AddScoped<ILogicaBug, LogicaBug>();
            services.AddScoped<ILogicaProyecto, LogicaProyecto>();
            services.AddScoped<ILogicaUsuario, LogicaUsuario>();
            services.AddScoped<ILogicaTarea, LogicaTarea>();

            services.AddScoped<IRepositorioBug, RepositorioBug>();
            services.AddScoped<IRepositorioProyecto, RepositorioProyecto>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioTarea, RepositorioTarea>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jiru.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jiru.Web v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<JiruDbContext>())
                {
                    context.Database.Migrate();
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
