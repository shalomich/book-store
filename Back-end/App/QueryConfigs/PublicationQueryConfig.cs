using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Entities.Publications;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QueryConfigs
{
    public class PublicationQueryConfig : ProductQueryConfig<Publication>
    {
        public PublicationQueryConfig()
        {
            CreateSorting("releaseYear", publication => publication.ReleaseYear);

            CreateFilter("releaseYear", publication => publication.ReleaseYear);
            CreateFilter("authorId", publication => publication.AuthorId);
            CreateFilter("publisherId", publication => publication.PublisherId);
            CreateFilter("typeId", publication => publication.TypeId.Value);
            CreateFilter("ageLimitId", publication => publication.AgeLimitId.Value);
            CreateFilter("coverArtId", publication => publication.CoverArtId.Value);
            CreateFilter("genreIds", publication => publication.GenresPublications
                .Select(genre => genre.Genre.Id));

            CreateSearch("author", entity => entity.Author.Name);
            CreateSearch("publisher", entity => entity.Publisher.Name);
        }
    }
}
