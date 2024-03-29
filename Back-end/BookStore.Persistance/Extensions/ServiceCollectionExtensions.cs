﻿using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Persistance.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssemblyName = typeof(ServiceCollectionExtensions).Assembly.GetName().Name;

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
                sqlOptions.MigrationsAssembly(currentAssemblyName));   
        });

        services.AddAsyncInitializer<DatabaseInitializer>();

        return services;
    }
}

