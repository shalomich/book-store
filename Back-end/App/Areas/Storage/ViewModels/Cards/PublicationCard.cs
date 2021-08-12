using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels.Cards
{
    public record PublicationCard : FullProductCard
    {
        public string Isbn { init; get; }
        public int ReleaseYear { init; get; }
        public string AuthorName { init; get; }
        public string PublisherName { init; get; }
        public string Type { init; get; }
        public string[] Genres { init; get; }
        public string OriginalName { init; get; }
        public string AgeLimit { init; get; }
        public string CoverArt { init; get; } 
        public string PublicationFormat { init; get; }    
        public int? PageQuantity { init; get; }
     
    }
}
