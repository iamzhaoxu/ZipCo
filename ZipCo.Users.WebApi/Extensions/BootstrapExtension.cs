using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ZipCo.Users.WebApi.Extensions
{
    public static class BootstrapExtension
    {
        public static void AddAppVersion(this IServiceCollection serviceCollection, ApiVersion apiVersion)
        {
            serviceCollection.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = apiVersion;
            });

            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = apiVersion;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddSwaggerOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Customer API",
                        Version = "v1",
                        Description = "Through this API you can access member and account details",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "iamzhaoxu@gmail.com",
                            Name = "Xu Zhao",
                        }
                    });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        public static void AddAppDataMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }


        public static void UseSwagger(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"{description.GroupName}/swagger.json", "ZipCo API");
                }
                 
            });
        }

        public static IHostBuilder ReadAppConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                builder.AddEnvironmentVariables();
            });
            return hostBuilder;
        }

        public static IHostBuilder ReadLogConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration));
            return hostBuilder;
        }
    }
}
