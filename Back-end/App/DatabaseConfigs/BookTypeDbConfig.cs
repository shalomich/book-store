using App.Entities.Books;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DatabaseConfigs
{
    public class BookTypeDbConfig : RelatedEntityDbConfig<BookType>
    {
        public override void Configure(EntityTypeBuilder<BookType> builder)
        {
            base.Configure(builder);

            builder.HasIndex(type => type.Name).IsUnique();

            builder.HasData(new BookType[]
            {
                new BookType {Id = 1, Name = "Художественная литература"},
                new BookType {Id = 2, Name = "Манга"},
                new BookType {Id = 3, Name = "Ранобэ"},
                new BookType {Id = 4, Name = "Графический роман"},
                new BookType {Id = 5, Name = "Артбук"}
            });
        }
    }
}
