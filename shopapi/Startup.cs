using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using MyFants.Entities;
using Microsoft.EntityFrameworkCore;
using MyFants.Repos;
using Microsoft.Extensions.Options;


namespace MyFants
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            // requires using Microsoft.Extensions.Options
            services.Configure<CommentsstoreDatabaseSettings>(
                Configuration.GetSection(nameof(CommentsstoreDatabaseSettings)));

            services.AddSingleton<ICommentsstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CommentsstoreDatabaseSettings>>().Value);

            services.AddSingleton<CommentsService>();

            services.AddControllers();

            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(Configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );
            //PG
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<PGEntities>(options =>
                    options.UseNpgsql(
                    Configuration["Config:PGConnectionString"]));
            services.AddScoped<PGRepo>();
            services.AddScoped<PGEntities>();

            //FB
            services.AddEntityFrameworkFirebird()
                .AddDbContext<FBEntities>(options =>
                    options.UseFirebird(
                    Configuration["Config:FBConnectionString"]));
            services.AddScoped<FBRepo>();
            services.AddScoped<FBEntities>();


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shop API",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "GitHub repository",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/Gravetar/My_BD"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/docs/{documentName}/swagger.json";
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiClientes v1"));

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
