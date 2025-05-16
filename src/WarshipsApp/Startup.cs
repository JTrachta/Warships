using System.Text.Json.Serialization;
using System.Text.Json;
using WarshipsApp.HealthChecks;
using WarshipsApp.ApplicationLayer.Extensions;
using WarshipsApp.InterfaceLayer.Extensions;
using WarshipsApp.Exceptions;
using Microsoft.AspNetCore.Http.Timeouts;

namespace WarshipsApp
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                }
            );

            services.AddProblemDetails();
            services.AddExceptionHandler<ExceptionHandler>();

            services.AddHttpContextAccessor();
            services.AddCustomHealthChecks();


            services.AddSwaggerGen(options => 
            { 
        
            });

            services.AddInfrastructureServices(Configuration);
            services.AddInterfaceServices();
            services.AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "WarshipsApp");
                c.ConfigObject.DisplayRequestDuration = true;
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandler();

            app.UseCustomHealthChecks();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
