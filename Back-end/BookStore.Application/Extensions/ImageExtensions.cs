using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using System;

namespace BookStore.Application.Extensions;
internal static class ImageExtensions
{
    public static string GetExtension(this Image image) => image.Format switch
    {
        ImageFormat.PNG => ".png",
        ImageFormat.JPEG => ".jpeg",
        _ => throw new ArgumentOutOfRangeException(nameof(ImageFormat))
    };
}

