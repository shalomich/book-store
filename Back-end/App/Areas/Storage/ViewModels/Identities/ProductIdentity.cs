using App.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels.Identities
{
    public record ProductIdentity : EntityIdentity
    {
        public ImageDto TitleImage { init; get; }
    }

}
