using Asp.Versioning;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Services;
using Services.Contracts;

namespace TeamTaskManagerApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

        public static void ConfigureController(this IServiceCollection services) =>
            services
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddControllers(config =>
                {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                })
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .AddApplicationPart(typeof(TeamTaskManager.Presentation.AssemblyReference).Assembly);

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<IAppLogger, AppLogger>();

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var file = $"{typeof(TeamTaskManager.Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
            var path = Path.Combine(AppContext.BaseDirectory, file);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Team Task Management API",
                    Version = "v1",
                    Description = "Team Task Management API by Ojo Toba R.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ojo Toba R.",
                        Email = "ojotobar@gmail.com",
                        Url = new Uri("https://github.com/ojotobar")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Team Task Management API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                Array.Empty<string>()
                            }
                });
                c.IncludeXmlComments(path);
            });
        }
    }
}
