using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record ImageDto(string Name, string Data, ImageFormat Format, 
        int Height, int Width);
}
