using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Tapioca.HATEOAS;
using WebApi.Business;
using WebApi.Business.Implementattions;
using WebApi.HyperMedia;
using WebApi.Model.Context;
using WebApi.Repository;
using WebApi.Repository.Generic;
using WebApi.Repository.Implementattions;
using WebApi.Security.Configuration;

namespace WebApi
{
    public class Startup
    {
        private readonly Microsoft.Extensions.Logging.ILogger logger;
        public IConfiguration configuration { get; }
        public IWebHostEnvironment environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment, ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // CORS 
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            //Connection to database
#if DEBUG
            var strconn = this.configuration["MySqlConnection:Local"];
#else
            var strconn = this.configuration["MySqlConnection:AzureInApp"];
#endif
            services.AddDbContext<MySQLContext>(options => options.UseMySql(strconn));

            //Adding Migrations Support
            ExecuteMigrations(strconn);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            //Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                //options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.EnableEndpointRouting = false;

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
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1",
                   new OpenApiInfo
                   {
                       Title = "Movie Collection",
                       Version = "v1"
                   });
           });

            //Dependency Injection
            services.AddScoped<IActorBusiness, ActorBusinessImpl>();
            services.AddScoped<IGenreBusiness, GenreBusinessImpl>();
            services.AddScoped<IDirectorBusiness, DirectorBusinessImpl>();

            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
            services.AddScoped<IFileBusiness, FileBusinessImpl>();

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
            //if (this.environment.IsDevelopment())
            //{
            try
            {
                using (var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(strconn))
                {
                    var evolve = new Evolve.Evolve(evolveConnection, msg => this.logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations", "db/dataset" },
                        IsEraseDisabled = true,
                        Encoding = Encoding.UTF8,
                        MetadataTableSchema = "movie"
                        //todo: Encoding not working for inserts
                        //ALTER TABLE database.table MODIFY COLUMN col VARCHAR(255)
                        //CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;
                    };
                    evolve.Migrate();
                };
            }
            catch (Exception ex)
            {
                this.logger.LogCritical("Database migration failed.", ex);
                throw;
            }
            //}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            LoggerFactory.Create(builder =>
            {
                builder.AddConfiguration(this.configuration.GetSection("Logging"));
                builder.AddDebug();
            });

            //Enable Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {

                if (string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Development"))
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Collection API v1 (DEBUG)");
                }
                else
                {
#if DEBUG
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Movie Collection API v1 (BETA)");
#else
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Movie Collection API v1");
#endif

                }
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
