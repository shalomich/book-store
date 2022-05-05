using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands;

public record CreateProductCommand(Product Product) : IRequest<int>;
internal class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private ApplicationContext Context { get; }
    private S3Storage S3Storage { get; }

    public CreateProductHandler(ApplicationContext context, S3Storage s3Storage)
    {
        Context = context;
        S3Storage = s3Storage;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productForCreation = request.Product;

        await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);

        await SaveProduct(productForCreation);
        await SaveImageFiles(productForCreation);

        await transaction.CommitAsync(cancellationToken);

        return productForCreation.Id;
    }

    private async Task SaveProduct(Product product)
    {
        try
        {
            await Context.AddAsync(product);
            await Context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new BadRequestException("Error of saving in database.", exception);
        }
    }

    private async Task SaveImageFiles(Product product)
    {
        var saveImageFileTasks = new List<Task>();

        foreach (var image in product.Album.Images)
        {
            var saveImageFileTask = Task.Run(() => S3Storage.PostAsync(product.Id, image.Id, image.Data));

            saveImageFileTasks.Add(saveImageFileTask);
        }

        await Task.WhenAll(saveImageFileTasks);
    }
}

