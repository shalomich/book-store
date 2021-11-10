using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record ImageDto(string Name, string Data,
        int Height, int Width)
    {
        [EnumDataType(typeof(ImageFormat))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageFormat Format { init; get; }
    }
}
