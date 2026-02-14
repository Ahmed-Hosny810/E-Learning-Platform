using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace E_learningPlatform.WebApi.Extensions
{
    public static class ApiExtensions
    {
            public static void AddSwaggerExtension(this IServiceCollection services)
            {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var provider = services
                    .BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = "E-Learning Platform API",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated
                            ? "This API version is deprecated."
                            : null
                    });
                }
            });
        }
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            var provider = ((WebApplication)app).Services
                .GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"E-Learning Platform API {description.GroupName.ToUpperInvariant()}"
                    );
                }
                options.RoutePrefix = "swagger";
            });
            }
        }
    }
