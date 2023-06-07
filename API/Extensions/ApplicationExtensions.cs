using API.Helpers;
using API.Response;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationExtensions
    {

        public static async Task ApplyMigrations(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    await context.Database.MigrateAsync();
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();

                    logger.LogError("An error occurred. Details: {error}", ex.Message);
                }
            }
        }

        /// <summary>
        /// Add db context to IOC container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config) 
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        
        }

        public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                    var response = new ApiResponse { ErrorMessage = "Api Validation error",ErrorResult = errors };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static IServiceCollection AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}
