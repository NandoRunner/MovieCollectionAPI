﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Business;
using WebApi.Business.Implementattions;
using WebApi.Repository;
using WebApi.Repository.Implementattions;
using WebApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using WebApi.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using WebApi.HyperMedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;

namespace WebApi
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			//Connection to database
            var strconn = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(strconn));

            //Adding Migrations Support
            ExecuteMigrations(strconn);

            //Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                //options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
            })
            .AddXmlSerializerFormatters();

            //HATEOAS filter definitions
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new ActorEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new DirectorEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new GenreEnricher());

            //Service inject
            services.AddSingleton(filterOptions);

            //Versioning
            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Add Swagger Service
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Movie Collection",
                        Version = "v1"
                    });
            });
            //Dependency Injection
            services.AddScoped<IActorBusiness, ActorBusinessImpl>();
            services.AddScoped<IGenreBusiness, GenreBusinessImpl>();
            services.AddScoped<IDirectorBusiness, DirectorBusinessImpl>();

			//Dependency Injection of GenericRepository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IRepositoryInter<>), typeof(GenericRepositoryInter<>));

            services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepositoryImpl<>));
            services.AddScoped(typeof(IViewMovieByRepository<>), typeof(ViewMovieByRepositoryImpl<>));

            services.AddScoped<IMovieBusiness, MovieBusinessImpl>();
            services.AddScoped<IMovieRepository, MovieRepositoryImpl>();
        }

        private void ExecuteMigrations(string strconn)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    //var devconn = _configuration["MySqlConnection:MySqlConnectionString"];
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(strconn);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "fandradetecinfo.utils/db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Enable Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Collection API v1");
            });

            //Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            //Adding map routing
            app.UseMvc(routes =>
			{
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}");
            });
        }
    }
}