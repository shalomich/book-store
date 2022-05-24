using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Persistance;

internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private ApplicationContext Context { get; }

    public DatabaseInitializer(
        ApplicationContext context)
    {
        Context = context;
    }

    public async Task InitializeAsync()
    {
        await Context.Database.MigrateAsync();
        await InitializeRolesAsync();
    }

    private async Task InitializeRolesAsync()
    {
        foreach (RoleName roleName in Enum.GetValues<RoleName>())
        {
            var roleNameString = roleName.ToString();

            if (await Context.Roles.AnyAsync(role => role.Name == roleNameString))
            {
                continue;
            }

            var userRole = new IdentityRole<int>
            {
                Name = roleNameString,
                NormalizedName = roleNameString.ToUpper(),
            };

            await Context.Roles.AddAsync(userRole);
            await Context.SaveChangesAsync();
        }
    }
}
