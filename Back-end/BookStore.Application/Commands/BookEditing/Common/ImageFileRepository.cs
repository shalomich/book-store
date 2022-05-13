using BookStore.Application.Extensions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.Common;
internal class ImageFileRepository
{
    private S3Storage S3Storage { get; }
    private ApplicationContext Context { get; }

    public ImageFileRepository(
        S3Storage s3Storage,
        ApplicationContext context)
    {
        S3Storage = s3Storage;
        Context = context;
    }

    public async Task AddImageFiles(IEnumerable<Image> images, CancellationToken cancellationToken)
    {
        if (!images.Any())
        {
            return;
        }

        var albumId = images.First().AlbumId;
        var ownerBook = await GetOwnerBook(albumId, cancellationToken);

        var saveImageFileTasks = new List<Task>();

        foreach (var image in images)
        {
            var path = CreateStoragePath(image, ownerBook);

            var bytes = Convert.FromBase64String(image.Data);
            
            var saveImageFileTask = Task.Run(async () => 
            {
                using var imageFileStream = new MemoryStream(bytes);

                await S3Storage.PostAsync(path, imageFileStream, cancellationToken);
            });

            saveImageFileTasks.Add(saveImageFileTask);
        }

        await Task.WhenAll(saveImageFileTasks);
    }

    public async Task RemoveImagesFiles(IEnumerable<Image> images, CancellationToken cancellationToken)
    {
        if (!images.Any())
        {
            return;
        }

        var albumId = images.First().AlbumId;
        var ownerBook = await GetOwnerBook(albumId, cancellationToken);

        var removeImageFileTasks = new List<Task>();

        foreach (var image in images)
        {
            var path = CreateStoragePath(image, ownerBook);

            var saveImageFileTask = Task.Run(() => S3Storage.RemoveAsync(path, cancellationToken));

            removeImageFileTasks.Add(saveImageFileTask);
        }

        await Task.WhenAll(removeImageFileTasks);
    }

    private async Task<Book> GetOwnerBook(int albumId, CancellationToken cancellationToken)
    {
        return await Context.Books
            .Include(book => book.Author)
            .Include(book => book.Publisher)
            .SingleAsync(book => book.Album.Id == albumId, cancellationToken);
    }

    private string CreateStoragePath(Image image, Book book)
    {
        var bookIdentifier = $"{book.Name} {book.Publisher.Name} {book.ReleaseYear} {book.Isbn}";

        return $"{book.Author.Name}/{bookIdentifier}/{image.Name}{image.GetExtension()}";
    }
}

