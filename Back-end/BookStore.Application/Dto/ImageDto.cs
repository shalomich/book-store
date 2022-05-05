using BookStore.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Dto
{
    public record ImageDto(int Id, string Name,
        int Height, int Width)
    {
        [EnumDataType(typeof(ImageFormat))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageFormat Format { init; get; }
        public string FileUrl { init; get; }
    }
}
