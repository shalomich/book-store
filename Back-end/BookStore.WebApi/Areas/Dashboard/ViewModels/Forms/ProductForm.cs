﻿
using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public record ProductForm : IEntityForm
    {
        public int Id { init; get; }

        [Required]
        public string Name { init; get; }

        [Required]
        [Range(Product.MinCost, int.MaxValue)]
        public int Cost { init; get; }

        [Required]
        public uint Quantity { set; get; }

        [MaxLength(Product.MaxDescriptionLength)]
        public string Description { set; get; }

        public AlbumForm Album { set; get; }
    }
}
