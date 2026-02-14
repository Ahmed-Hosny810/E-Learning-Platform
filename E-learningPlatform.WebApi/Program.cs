
using Asp.Versioning;
using E_learningPlatform.Application;
using E_learningPlatform.Application.Settings;
using E_learningPlatform.Infrastructure.Persistence;
using E_learningPlatform.WebApi.Extensions;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

namespace E_learningPlatform.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers with JSON enum string conversion
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters
                        .Add(new JsonStringEnumConverter());
                });

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationLayer();

            builder.Services.Configure<FileUploadSettings>(
                builder.Configuration.GetSection("FileUploadSettings"));

            // API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new QueryStringApiVersionReader("api-version")
                );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Swagger (via extension)
            builder.Services.AddSwaggerExtension();

            var app = builder.Build();

            var physicalPath = Path.Combine(
                builder.Environment.ContentRootPath, "App_Data", "uploads");

            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension(); 
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(physicalPath),
                RequestPath = "/App_Data/uploads",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Accept-Ranges", "bytes");
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
                }
            });

            app.MapControllers();
            app.Run();
        }
    }
}

