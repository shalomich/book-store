using App.Areas.Storage.Attributes.FormModel;
using App.Entities;
using App.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public record ProductForm : EntityForm
    {
        [Required]
        [FormField(FormFieldType.Text, "Название товара")]
        public string Name { init; get; }

        [Required]
        [Range(Product.MinCost, int.MaxValue)]
        [FormField(FormFieldType.Number, "Цена")]
        public int Cost { init; get; }

        [Required]
        [FormField(FormFieldType.Number, "Количество товара")]
        public uint Quantity { set; get; }

        [MaxLength(Product.MaxDescriptionLength)]
        [FormField(FormFieldType.TextArea, "Описание",false)]
        public string Description { set; get; }

        [FormField(FormFieldType.Image, "Изображения товара",false)]
        public AlbumForm Album { set; get; }
    }
}
