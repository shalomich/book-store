using App.Areas.Storage.ViewModels;
using App.Entities;
using App.Entities.Publications;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage
{
    public class PublicationSortingConfig : EntitySortingConfig<Publication>
    {
        public PublicationSortingConfig()
        {
            CreateSorting<Publication,int>(publication => publication.Cost);
            CreateSorting<Publication, string>(publication => publication.Type.Name, "type");
        }
    }
}
