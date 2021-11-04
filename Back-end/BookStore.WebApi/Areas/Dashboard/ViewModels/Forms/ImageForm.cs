using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public class ImageForm : IEquatable<ImageForm>
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

        public override bool Equals(object obj)
        {
            return obj is Image other
                && Equals(other);
        }

        public bool Equals(ImageForm other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
