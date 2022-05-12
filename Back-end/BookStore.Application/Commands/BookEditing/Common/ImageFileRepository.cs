using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.Common;
internal class ImageFileRepository
{
    private S3Storage S3Storage { get; }

    public ImageFileRepository(S3Storage s3Storage)
    {
        S3Storage = s3Storage;
    }

    public async Task AddImageFiles(IEnumerable<Image> images, int bookId, CancellationToken cancellationToken)
    {
        var saveImageFileTasks = new List<Task>();

        foreach (var image in images)
        {
            var path = CreateStoragePath(bookId, image.Id);

            var bytes = Convert.FromBase64String(image.Data);
            using var imageFileStream = new MemoryStream(bytes);

            var saveImageFileTask = Task.Run(() => S3Storage.PostAsync(path, imageFileStream, cancellationToken));

            saveImageFileTasks.Add(saveImageFileTask);
        }

        await Task.WhenAll(saveImageFileTasks);
    }

    public async Task RemoveImagesFiles(IEnumerable<Image> images, int bookId, CancellationToken cancellationToken)
    {
        var removeImageFileTasks = new List<Task>();

        foreach (var image in images)
        {
            var path = CreateStoragePath(bookId, image.Id);

            var saveImageFileTask = Task.Run(() => S3Storage.RemoveAsync(path, cancellationToken));

            removeImageFileTasks.Add(saveImageFileTask);
        }

        await Task.WhenAll(removeImageFileTasks);
    }

    private string CreateStoragePath(int bookId, int imageId)
    {
        return $"{bookId}/{imageId}";
    }
}

