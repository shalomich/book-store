using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Persistance.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration, 
        bool isDevelopment)
    {
        var currentAssemblyName = typeof(ServiceCollectionExtensions).Assembly.GetName().Name;

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationContext>(options =>
        {
            if (isDevelopment)
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly(currentAssemblyName));
            }
            else
            {
                options.UseNpgsql(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly(currentAssemblyName));
            }
        });

        services.AddAsyncInitializer<DatabaseInitializer>();

        return services;
    }
}

