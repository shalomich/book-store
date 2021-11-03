using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record ImageDto(string Name, string Data, string Format, int Height, int Width);
}
