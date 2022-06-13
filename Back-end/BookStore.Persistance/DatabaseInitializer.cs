using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Persistance.Data;
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
        await InitializeBookRelatedEntities();
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

    private async Task InitializeBookRelatedEntities()
    {
        var defaultData = await GetDataFromFile();

        await TryAddBookRelatedEntities(Context.Genres, defaultData.Genres);
        await TryAddBookRelatedEntities(Context.BookTypes, defaultData.BookTypes);
        await TryAddBookRelatedEntities(Context.AgeLimits, defaultData.AgeLimits);
        await TryAddBookRelatedEntities(Context.CoverArts, defaultData.CoverArts);
        await TryAddBookRelatedEntities(Context.TagGroups, defaultData.TagGroups);
    }

    private async Task<DefaultBookRelatedEntities> GetDataFromFile()
    {
        var resourceFileName = "BookRelatedEntities.json";

        var resourceNamespace = typeof(DefaultBookRelatedEntities).Namespace;
        var resource = $"{resourceNamespace}.{resourceFileName}";

        var assembly = Assembly.GetExecutingAssembly();
        using var resourceFile = assembly.GetManifestResourceStream(resource);

        if (resource is null)
        {
            throw new InvalidOperationException($"Cannot to load the file with teeth {resource}.");
        }

        using var reader = new StreamReader(resourceFile);

        var resourceJson = await reader.ReadToEndAsync();

        return JsonSerializer.Deserialize<DefaultBookRelatedEntities>(resourceJson);
    }

    private async Task TryAddBookRelatedEntities(IQueryable<RelatedEntity> relatedEntityQuery, IEnumerable<RelatedEntity> relatedEntities)
    {
        var relatedEntityNames = await relatedEntityQuery
            .Select(relatedEntity => relatedEntity.Name)
            .ToListAsync();

        var relatedEntitiesToAdd = relatedEntities
            .Where(relatedEntity => !relatedEntityNames.Contains(relatedEntity.Name))
            .ToList();

        if (!relatedEntitiesToAdd.Any())
        {
            return;
        }

        Context.AddRange(relatedEntitiesToAdd);
        await Context.SaveChangesAsync();
    }
}
