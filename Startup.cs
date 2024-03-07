namespace ListToDo
{
    using Entities.DataContext;
    using ListToDo.Repositories;
    using ListToDo.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public  class Startup 
    {
      

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            //Environment.GetEnvironmentVariable("sqldb_connection")
            services.AddDbContext<ModelDbContext>(x => x.UseSqlServer("Server = GIOPLAZAS;initial catalog=todo; User Id=sa;Password=1995;"));
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Aplicar migraciones si es necesario al iniciar la aplicación
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ModelDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Manejar errores si la migración falla
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error al aplicar migraciones.");
                }
            }

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Mapa de rutas para controladores
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
