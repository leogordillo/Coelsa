using AutoMapper;
using Coelsa.Challenge.Api.Aplication;
using Coelsa.Challenge.Api.Data.Cnx;
using Coelsa.Challenge.Api.Data.Repository;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Coelsa.challenge
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
            ConfigureServiceCollections(services);

            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ContactAdd>())
                                     .SetCompatibilityVersion(CompatibilityVersion.Latest); ; 

            ConfigureServiceLibraries(services);

            ConfigureServiceDocumentation(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "COELSA Challenge V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureServiceCollections(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IContactRepository, ContactRepository>();
        }

        private static void ConfigureServiceLibraries(IServiceCollection services)
        {
            //# Librería MediatR
            // Esta línea se define una sola vez -el servicio empieza a reconocer a MediatR-
            services.AddMediatR(typeof(ContactAdd.Manejador).Assembly);

            //# Librería Automapper
            // Esta línea se define una sola vez -el servicio empieza a reconocer a Automapper-
            services.AddAutoMapper(typeof(CompanyFilter.Manejador));
        }

        private static void ConfigureServiceDocumentation(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "COELSA Challenge - Leandro Gordillo Calvo",
                    Description = "COELSA Challenge - API Rest .Net Core 3.1",
                    TermsOfService = new System.Uri("https://coelsa.com.ar"),
                    Contact = new OpenApiContact
                    {
                        Name = "Leandro Gordillo Calvo",
                        Email = "leandro.gordillo@gmail.com",
                        Url = new System.Uri("https://www.linkedin.com/in/leandrogordillo/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new System.Uri("https://www.linkedin.com/in/leandrogordillo/")
                    }
                }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRules();
            });
        }
    }
}
