using App.Areas.Store.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels
{
    public record ProductCardsByQuery
    {
        public ProductCard[] Cards { init; get; }
        public string[] QueryErrors { init; get; }
    }
}
