using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.BookEditing.Common;
public record ImageForm
{
    [Required]
    public string Name { init; get; }

    [Required]
    [EnumDataType(typeof(ImageFormat))]
    [JsonConverter(typeof(StringEnumConverter))]
    public ImageFormat Format { init; get; }

    [Required]
    public string Data { init; get; }

    [Required]
    [Range(Image.MinHeight, int.MaxValue)]
    public int Height { init; get; }

    [Required]
    [Range(Image.MinWidth, int.MaxValue)]
    public int Width { init; get; }
}
