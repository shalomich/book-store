﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Basket
{
    public record AddingBasketProduct
    {
        [Required]
        public int? ProductId { init; get; }
    }
}
