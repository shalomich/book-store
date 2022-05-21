﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record CardDto
    {
        public int Id { init; get; }
        public string Name { init; get; }
        public int Cost { init; get; }
        public ImageDto TitleImage { init; get; }
        public int Quantity { init; get; }
        public int? DiscountPercentage { init; get; }
        public string Description { init; get; }
        public IEnumerable<ImageDto> NotTitleImages { init; get; }
        public string Isbn { init; get; }
        public int ReleaseYear { init; get; }
        public string AuthorName { init; get; }
        public string PublisherName { init; get; }
        public string Type { init; get; }
        public IEnumerable<string> Genres { init; get; } = Enumerable.Empty<string>();
        public IEnumerable<string> Tags { init; get; } = Enumerable.Empty<string>();
        public string OriginalName { init; get; }
        public string AgeLimit { init; get; }
        public string CoverArt { init; get; }
        public string BookFormat { init; get; }
        public int? PageQuantity { init; get; }
    }
}
