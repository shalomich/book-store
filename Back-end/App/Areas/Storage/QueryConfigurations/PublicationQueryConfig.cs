using App.Areas.Storage.ViewModels;
using App.Entities;
using App.Entities.Publications;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.QueryConfigurations
{
    public class PublicationQueryConfig : ProductQueryConfig<Publication>
    {
        public PublicationQueryConfig()
        {
            CreateSorting("releaseYear", publication => publication.ReleaseYear);

            CreateFilter("releaseYear", publication => publication.ReleaseYear);
            CreateFilter("authorId", publication => publication.AuthorId);
            CreateFilter("publisherId", publication => publication.PublisherId);
            CreateFilter("type", publication => publication.Type.Name);
            CreateFilter("ageLimit", publication => publication.AgeLimit.Name);
            CreateFilter("genres", publication => publication.GenresPublications
                .Select(genre => genre.Genre.Name));

            CreateSearch("author", entity => entity.Author.Name);
            CreateSearch("publisher", entity => entity.Publisher.Name);
        }
    }
}
