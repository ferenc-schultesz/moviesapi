using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviesAPI.Entities;
using MoviesAPI.Models;
using MoviesAPI.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MoviesAPI
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
            services.AddMvc();

            //At deployment connection
            //var connectionString = Environment.GetEnvironmentVariable("moviesDBProdConnectionString");

            var connectionString = Configuration["connectionStrings:moviesDBConnectionString"];
            services.AddDbContext<MoviesContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Movies API",
                    Version = "v1",
                    Description = "Technical Exercise solution to expose data about Movies through an API "
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MoviesContext moviesContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug(LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();

            // Populate db
            moviesContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Movie, MovieDto>();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API");
                c.RoutePrefix = string.Empty;
            });

        }
    }
   
}
