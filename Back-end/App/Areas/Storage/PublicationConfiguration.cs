using App.Entities;
using App.Entities.Publications;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage
{
    public class PublicationConfiguration : SortingConfiguration<Publication>
    {
        public PublicationConfiguration()
        {
            CreateSorting(publication => publication.Cost);
            CreateSorting(publication => publication.Type.Name);
        }
    }
}
