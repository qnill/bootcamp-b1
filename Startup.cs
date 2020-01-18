using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using net_core_bootcamp_b1.Database;
using net_core_bootcamp_b1.Services;
using System;
using System.IO;
using System.Reflection;

namespace net_core_bootcamp_b1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bootcamp B1 API",
                    Description = ".NET Core Bootcamp B1 2020",
                    License = new OpenApiLicense
                    {
                        Name = "Develop by Anýl Yýldýrým",
                        Url = new Uri("https://stackoverflow.com/users/7612201/anilyildirim")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.swagger.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            services.AddDbContext<BootcampDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("BootcampDbConnection")));

            // DI
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IEventService, EventService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Core Bootcamp B1 API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
